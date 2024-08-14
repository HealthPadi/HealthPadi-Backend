using HealthPadiWebApi.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HealthPadiWebApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto);
        Task<LoginResponseDto> LoginUserWithGoogleAsync(string email, string firstName, string lastName);
        
    }
}
