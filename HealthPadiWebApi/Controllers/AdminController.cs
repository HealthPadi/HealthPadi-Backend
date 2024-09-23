using HealthPadiWebApi.DTOs.Shared;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthPadiWebApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("toggle-user-status/{userId}")]
        public async Task<IActionResult> ToggleUserStatus(Guid userId)
        {
            var result = await _userService.ToggleUserStatusAsync(userId);

            if (!result)
            {
                return NotFound(ApiResponse.NotFoundException("User not found"));
            }

            return Ok(ApiResponse.SuccessMessage("User status updated successfully"));
        }

    }
}