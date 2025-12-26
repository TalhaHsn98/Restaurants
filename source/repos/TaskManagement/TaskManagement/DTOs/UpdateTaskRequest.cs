using TaskManagement.Core.Domain;

namespace TaskManagement.API.DTOs
{
    public sealed class UpdateTaskRequest
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskManagement.Core.Domain.TaskStatus Status { get; set; }
    }
}
