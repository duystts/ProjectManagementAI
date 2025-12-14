using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectManagement.Entities.Models.Enums;
using TaskStatusEnum = ProjectManagement.Entities.Models.Enums.TaskStatus;

namespace ProjectManagement.Entities.Models
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskStatusEnum Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        [ForeignKey("AssignedUser")]
        public int? AssignedUserId { get; set; }

        public User? AssignedUser { get; set; }

        public ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
    }
}