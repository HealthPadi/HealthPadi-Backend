using AutoMapper;
using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthPadiWebApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.Include(u => u.Reports).ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<ReportDto>> GetReportsByUserIdAsync(Guid userId)
        {
            var user = await _userManager.Users.Include(u => u.Reports).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            if (user.Reports == null || !user.Reports.Any())
            {
                return new List<ReportDto>();
            }
            return _mapper.Map<List<ReportDto>>(user.Reports);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
           var user = await _userManager.Users.Include(u => u.Reports).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid userId)
        {
           var user = await _userManager.Users.Include(u => u.Reports).FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<bool> ToggleUserStatusAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return false;
            }
            // Check if the user is currently enabled (not disabled) and is about to be disabled
            if (!user.IsDisabled && user.Point > 0)
            {
                user.Point = (int)Math.Floor(user.Point / 2.0);
            }

            // Toggle the IsDisabled flag
            user.IsDisabled = !user.IsDisabled;

            // Update the user in the database
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;

        }
    }
}