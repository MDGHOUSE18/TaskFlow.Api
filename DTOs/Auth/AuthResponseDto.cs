namespace TaskFlow.Api.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public int ExpiresIn { get; set; }
        public string Name { get; set; } = null!;
    }
}
