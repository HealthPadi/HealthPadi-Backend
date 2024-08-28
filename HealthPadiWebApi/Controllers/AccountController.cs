using AutoMapper;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2.Requests;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.DTOs.Shared;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Internal;
using System.Net.Http;


namespace HealthPadiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger, IMapper mapper, IConfiguration configuration, HttpClient httpClient, UserManager<User> userManager)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        /**
         * Register: This method is used to register a new user
         * @param registerRequestDto: The request body containing the user details
         * Returns IActionResult: Returns a success message if registration is successful, else returns an error message
         */
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await _accountService.RegisterUserAsync(registerRequestDto);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(registerRequestDto.Email);
                return Ok(ApiResponse.SuccessMessageWithData(_mapper.Map<RegisterResponseDto>(user)));
            }
            if (result.Errors.Any(e => e.Description == ErrorMessages.EmailAlreadyRegistered))
            {
                return BadRequest(ErrorMessages.EmailAlreadyRegistered);
            }

            return BadRequest(ApiResponse.UnknownException("Something went wrong, try again"));
        }


        //Post: /api/account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var response = await _accountService.LoginUserAsync(loginRequestDto);

            if (response != null)
            {
                return Ok(ApiResponse.SuccessMessageWithData(response));
            }
            return Unauthorized(ApiResponse.AuthenticationException("Invalid email or password"));
        }

        /**
         * GoogleLogin: This method is used to login a user using Google OAuth2.0
         * 
         * @param authCode: The authorization code received from Google to be exchanged for an accessToken
         * @return IActionResult: Returns the login response if successful, else returns an error message
         */
        [HttpPost]
        [Route("google-login")]
        public async Task<IActionResult> GoogleLogin([FromQuery] string authCode)
        {
            try
            {
                var tokenResponse = await GetGoogleTokenAsync(authCode);
                if (tokenResponse == null)
                {
                    _logger.LogError("Couldn't retrieve Token request");
                    return BadRequest("Couldn't get IdToken from Google");
                }

                var payload = await ValidateGoogleTokenAsync(tokenResponse.IdToken);
                if (payload == null)
                {
                    _logger.LogError("Couldn't retrieve user details from Google");
                    return BadRequest("Couldn't retrieve user details from Google");
                }

                if (string.IsNullOrEmpty(payload.Email) || string.IsNullOrEmpty(payload.GivenName) || string.IsNullOrEmpty(payload.FamilyName))
                {
                    return BadRequest("Incomplete user information received from Google.");
                }

                var loginResponse = await _accountService.LoginUserWithGoogleAsync(payload.Email, payload.GivenName, payload.FamilyName);

                if (loginResponse != null)
                {
                    return Ok(ApiResponse.SuccessMessageWithData(loginResponse));
                }

                return BadRequest("Login Failed");
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "HTTP request error while getting user details from Google");
                return StatusCode(503, "Service Unavailable");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting user details from Google");
                return BadRequest("Error Getting user details from Google");
            }
        }

        /**
         * GetGoogleTokenAsync: This method is used to get the Google OAuth2.0 token
         * 
         * @param authCode: The authorization code received from Google to be exchanged for an accessToken
         * @return TokenResponse: Returns the token response from Google
         */
        private async Task<TokenResponse> GetGoogleTokenAsync(string authCode)
        {
            var clientId = _configuration["Authentication:Google:ClientId"];
            var clientKey = _configuration["Authentication:Google:ClientKey"];
            var redirectUri = _configuration["Authentication:Google:RedirectUri"];
            var tokenRequest = new AuthorizationCodeTokenRequest
            {
                ClientId = clientId,
                ClientSecret = clientKey,
                Code = authCode,
                RedirectUri = redirectUri,
                GrantType = "authorization_code"
            };

            string tokenServerUrl = "https://oauth2.googleapis.com/token";
            var systemClock = Google.Apis.Util.SystemClock.Default;

            return await tokenRequest.ExecuteAsync(_httpClient, tokenServerUrl, CancellationToken.None, systemClock);
        }

        /**
         * ValidateGoogleTokenAsync: This method is used to validate the Google OAuth2.0 token
         * 
         * @param idToken: The idToken received from Google
         * @return Payload: Returns the payload of the token containing user details
         */
        private async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken)
        {
            return await GoogleJsonWebSignature.ValidateAsync(idToken);
        }

    }
}