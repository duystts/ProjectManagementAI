using ProjectManagement.Entities.Models.Enums;
using TaskStatusEnum = ProjectManagement.Entities.Models.Enums.TaskStatus;

namespace ProjectManagement.Entities.Models.DTOs
{
    public class UpdateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskPriority Priority { get; set; }
        public TaskStatusEnum Status { get; set; }
    }

    public class UpdateStatusRequest
    {
        public TaskStatusEnum Status { get; set; }
    }
}