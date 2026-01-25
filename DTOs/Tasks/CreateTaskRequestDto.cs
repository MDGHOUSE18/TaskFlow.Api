using System.ComponentModel.DataAnnotations;
using TaskFlow.Api.Domain.Entities;

namespace TaskFlow.Api.DTOs.Tasks
{
    public class CreateTaskRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        public TaskPriority Priority { get; set; } 
        public DateTime? DueDate { get; set; } = null!;

    }
}
