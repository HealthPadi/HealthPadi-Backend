using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.DTOs.Shared;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthPadiWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(ApiResponse.SuccessMessageWithData(user));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(ApiResponse.SuccessMessageWithData(users));
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found with the given email");
            }
            return Ok(ApiResponse.SuccessMessageWithData(user));
        }

        [HttpGet("{userId}/reports")]
        public async Task<IActionResult> GetReportsByUserId(Guid userId)
        {
            var reports = await _userService.GetReportsByUserIdAsync(userId);

            // Handle if the user is not found
            if (reports == null)
            {
                return NotFound("User not found");
            }

            // Handle if the user exists but has no reports
            if (!reports.Any())
            {
                return Ok(ApiResponse.SuccessMessage("No reports found for this user."));
            }

            // Return the reports if they exist
            return Ok(ApiResponse.SuccessMessageWithData(reports));

        }
    }
}