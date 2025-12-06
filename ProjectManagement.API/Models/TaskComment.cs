using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.API.Models
{
    public class TaskComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public bool IsAiGenerated { get; set; }

        [ForeignKey("ProjectTask")]
        public int ProjectTaskId { get; set; }

        public ProjectTask ProjectTask { get; set; } = null!;
    }
}