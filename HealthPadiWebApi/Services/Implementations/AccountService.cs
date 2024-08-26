using AutoMapper;
using HealthPadiWebApi.DTOs;
using HealthPadiWebApi.DTOs.Response;
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

        /**
         * LoginUserWithGoogleAsync - Completes a user's log in request using Google.
         *                            If user doesn't exist an account is created for them
         * @param email - the email of the user
         * @param firstName - the first name of the user
         * @param lastName - the last name of the user
         * @return a LoginResponseDto containing the JWT token
         */
        public async Task<LoginResponseDto> LoginUserWithGoogleAsync(string email, string firstName, string lastName)
        {
           try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = email,
                        Email = email,
                        Firstname = firstName,
                        Lastname = lastName
                    };

                    var identityResult = await _userManager.CreateAsync(user);

                    if (identityResult.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    else
                    {
                        throw new Exception("User creation failed.");
                    }
                }

                var roles = await _userManager.GetRolesAsync(user);
                var jwtToken = _tokenService.CreateJWTToken(user, roles.ToList());

                return new LoginResponseDto
                {
                    JwtToken = jwtToken
                };

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Signing in user failed");
            }

        }

        /* public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequestDto)
         {
             var existingUser = await _userManager.FindByEmailAsync(registerRequestDto.Email);
             if (existingUser != null)
             {
                 // Return an error indicating that the email is already in use
                 return IdentityResult.Failed(new IdentityError { Description = ErrorMessages.EmailAlreadyRegistered});
             }
             var user = _mapper.Map<User>(registerRequestDto);
             var identityResult = await _userManager.CreateAsync(user, registerRequestDto.Password);
             if (identityResult.Succeeded)
             {
                 // Assign default role if no roles are provided
                 if (registerRequestDto.Roles == null || !registerRequestDto.Roles.Any())
                 {
                     identityResult = await _userManager.AddToRoleAsync(user, "User");
                 }
                 else
                 {
                     // Assign the roles provided
                     identityResult = await _userManager.AddToRolesAsync(user, registerRequestDto.Roles);
                 }
             }

             return identityResult;
         }*/
        public async Task<IdentityResult> RegisterUserAsync(RegisterRequestDto registerRequestDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerRequestDto.Email);
            if (existingUser != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = ErrorMessages.EmailAlreadyRegistered });
            }

            var user = _mapper.Map<User>(registerRequestDto);
            var identityResult = await _userManager.CreateAsync(user, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Automatically assign the default "User" role
                identityResult = await _userManager.AddToRoleAsync(user, "User");
            }

            return identityResult;
        }

    }

}