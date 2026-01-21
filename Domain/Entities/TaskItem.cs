namespace TaskFlow.Api.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
