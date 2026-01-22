using TaskFlow.Api.Domain.Entities;

namespace TaskFlow.Api.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        int GetTokenExpiryMinutes();
    }
}
