using HealthPadiWebApi.DTOs.Response;
using HealthPadiWebApi.Models;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ToggleUserStatusAsync(Guid userId);
        Task<UserDto> GetUserByIdAsync(Guid userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<List<ReportDto>> GetReportsByUserIdAsync(Guid userId); // Assuming a Report class exists
    }
}
