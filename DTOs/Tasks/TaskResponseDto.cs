using TaskFlow.Api.Domain.Entities;

namespace TaskFlow.Api.DTOs.Tasks
{
    public class TaskResponseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public Domain.Entities.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
