namespace TaskFlow.Api.Domain.Entities
{
    public class ApplicationError
    {
        public Guid Id { get; set; }

        public string Message { get; set; } = null!;
        public string? StackTrace { get; set; }
        public string Source { get; set; } = null!;
        public string? Path { get; set; }

        public string? UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
