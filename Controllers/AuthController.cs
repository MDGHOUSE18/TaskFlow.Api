using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.Application.Interfaces;
using TaskFlow.Api.Common;
using TaskFlow.Api.DTOs.Auth;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {

            AuthResponseDto result = await _authService.LoginAsync(requestDto);
            return Ok(result);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(ApiResponse<RegisterResponseDto>.Ok(result));

        }



    }
}
