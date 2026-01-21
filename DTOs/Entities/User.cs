using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Api.DTOs.Entities
{
    public class User:IdentityUser
    {
        public string Name { get; set; } = null!;
    }
}
