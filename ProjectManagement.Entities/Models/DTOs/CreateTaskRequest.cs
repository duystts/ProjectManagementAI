using ProjectManagement.Entities.Models.Enums;

namespace ProjectManagement.Entities.Models.DTOs
{
    public class CreateTaskRequest
    {
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskPriority Priority { get; set; }
        public DateTime? Deadline { get; set; }
    }
}