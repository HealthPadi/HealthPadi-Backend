using AutoMapper;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.Models;
using HealthPadiWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthPadiWebApi.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> LoginUserAsync(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = _tokenService.CreateJWTToken(user, roles.ToList());

                        return new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                    }
                }
            }

            return null;
        }
        public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            var user = _mapper.Map<User>(registerRequestDto);
            var identityResult = await _userManager.CreateAsync(user, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(user, registerRequestDto.Roles);
                }
            }

            return identityResult;
        }
    }
    
}
