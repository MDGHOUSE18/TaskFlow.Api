using TaskFlow.Api.Common;
using TaskFlow.Api.DTOs.Auth;

namespace TaskFlow.Api.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request);
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request);


    }
}
