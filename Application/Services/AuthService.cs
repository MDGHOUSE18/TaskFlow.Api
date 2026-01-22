using Microsoft.AspNetCore.Identity;
using TaskFlow.Api.Application.Interfaces;
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

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
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
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            return new RegisterResponseDto
            {
                Email = user.Email!,
                Message = $"User registered successfully with email {user.Email}"
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user ==null)
            {
                throw new InvalidOperationException("User not exist with this email.");
            }

            var result =await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            var token = _tokenService.GenerateAccessToken(user);
            return new AuthResponseDto
            {
                AccessToken = token,
                ExpiresIn = 0,
                Name = user.Name
            };
        }

    }
}
