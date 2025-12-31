using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagement.Entities.Models
{
    public class TaskAttachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FilePath { get; set; } = string.Empty;

        [Required]
        public string FileType { get; set; } = string.Empty; // image, video

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        [ForeignKey("Task")]
        public int TaskId { get; set; }

        public ProjectTask Task { get; set; } = null!;

        [ForeignKey("UploadedBy")]
        public int UploadedByUserId { get; set; }

        public User UploadedBy { get; set; } = null!;
    }
}