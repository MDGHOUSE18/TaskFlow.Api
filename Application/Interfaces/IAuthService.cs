using TaskFlow.Api.DTOs.Auth;

namespace TaskFlow.Api.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    
    }
}
