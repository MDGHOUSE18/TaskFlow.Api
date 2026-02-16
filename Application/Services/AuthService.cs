using Microsoft.AspNetCore.Identity;
using TaskFlow.Api.Application.Interfaces;
using TaskFlow.Api.Common;
using TaskFlow.Api.Domain.Entities;
using TaskFlow.Api.DTOs.Auth;

namespace TaskFlow.Api.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return ApiResponse<RegisterResponseDto>
                    .Fail("User with this email already exists.");
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.Email,
                Name = request.Name
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ",
                    result.Errors.Select(e => e.Description));

                return ApiResponse<RegisterResponseDto>.Fail(errors);
            }

            return ApiResponse<RegisterResponseDto>.Ok(new RegisterResponseDto
            {
                Email = user.Email!,
                Message = $"User registered successfully with email {user.Email}"
            });
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return ApiResponse<AuthResponseDto>
                    .Fail("Invalid email address. Please enter a valid email.");
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return ApiResponse<AuthResponseDto>
                    .Fail("Invalid email or password.");
            }

            var token = _tokenService.GenerateAccessToken(user);

            return ApiResponse<AuthResponseDto>.Ok(new AuthResponseDto
            {
                AccessToken = token,
                ExpiresIn = 0,
                Name = user.Name
            });
        }

    }
}
