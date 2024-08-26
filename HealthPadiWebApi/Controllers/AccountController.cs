using AutoMapper;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Implementations;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //Post: /api/account/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await _accountService.RegisterUserAsync(registerRequestDto);

            if (result.Succeeded)
            {
                return Ok("User registered successfully, Please login");
            }
            if (result.Errors.Any(e => e.Description == ErrorMessages.EmailAlreadyRegistered))
            {
                return BadRequest(ErrorMessages.EmailAlreadyRegistered);
            }

            return BadRequest("Something went wrong");
        }

        //Post: /api/account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var response = await _accountService.LoginUserAsync(loginRequestDto);

            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest("Username or Password Incorrect");
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