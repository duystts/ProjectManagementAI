using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectManagement.API.Models.Enums;

namespace ProjectManagement.API.Models
{
    public class ProjectTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Models.Enums.TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public Project Project { get; set; } = null!;

        public ICollection<TaskComment> TaskComments { get; set; } = new List<TaskComment>();
    }
}