using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Entities.Models;

public class Knowledge
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    public string Content { get; set; } = string.Empty;
    
    public string Embedding { get; set; } = string.Empty; // JSON array of floats
    
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public int? TaskId { get; set; }
    public ProjectTask? Task { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}