using System.ComponentModel.DataAnnotations;
using TaskFlow.Api.Domain.Entities;

namespace TaskFlow.Api.DTOs.Tasks
{
    public class UpdateTaskRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;
        [Required]
        public string? Description { get; set; }
        [Required]
        public Domain.Entities.TaskStatus Status { get; set; }
        [Required]
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate {  get; set; }
    }
}
