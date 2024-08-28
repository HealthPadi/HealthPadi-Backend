using AutoMapper;
using HealthPadiWebApi.DTOs.Request;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.DTOs.Shared;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthPadiWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountController(IAccountService accountService, IMapper mapper, UserManager<User> userManager)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //Post: /api/account/register
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

        [HttpGet]
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = "api/account/google-response" };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!result.Succeeded || result.Principal == null)
            {
                return BadRequest("Authentication failed.");
            }
            var claimsIdentity = result.Principal.Identity as ClaimsIdentity;
            var email = claimsIdentity?.FindFirst(ClaimTypes.Email)?.Value;
            var firstName = claimsIdentity?.FindFirst(ClaimTypes.GivenName)?.Value;
            var lastName = claimsIdentity?.FindFirst(ClaimTypes.Surname)?.Value;

            Console.WriteLine($"{email} >>>> {firstName} >>> {lastName}");

            if (email == null || firstName == null || lastName == null)
                return BadRequest("Incomplete information");

            var loginResponse = await _accountService.LoginUserWithGoogleAsync(email, firstName, lastName);

            if (loginResponse != null)
            {
                return Ok(loginResponse);
            }

            return BadRequest("Login Failed");
        }
    }
}