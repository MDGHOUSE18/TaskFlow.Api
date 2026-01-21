using Microsoft.AspNetCore.Identity;

namespace TaskFlow.Api.Domain.Entities
{
    public class User:IdentityUser
    {
        public string Name { get; set; } = null!;
    }
}
