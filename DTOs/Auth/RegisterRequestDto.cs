using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.DTOs.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
    }
}
