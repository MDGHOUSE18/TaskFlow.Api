using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Api.Application.Interfaces;
using TaskFlow.Api.Domain.Entities;

namespace TaskFlow.Api.Application.Services
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));

            var credentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var tokenExpires = DateTime.UtcNow.AddMinutes(GetTokenExpiryMinutes());

            var token = new JwtSecurityToken(
                issuer : jwtSettings["Issuer"],
                audience : jwtSettings["Audience"],
                claims : claims,
                expires:tokenExpires,
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int GetTokenExpiryMinutes()
        {
            return int.Parse(_configuration["Jwt:ExpiryMinutes"]!);
        }
    }
}
