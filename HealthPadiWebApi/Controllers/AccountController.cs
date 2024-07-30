using AutoMapper;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
