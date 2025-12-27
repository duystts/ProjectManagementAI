namespace ProjectManagement.Entities.Models.DTOs;

public class ChatRequest
{
    public string Message { get; set; } = string.Empty;
    public int? ProjectId { get; set; }
    public int? TaskId { get; set; }
}

public class ChatResponse
{
    public string Response { get; set; } = string.Empty;
    public List<string> Sources { get; set; } = new();
}

public class EmbeddingResult
{
    public float[] Vector { get; set; } = Array.Empty<float>();
}

public class EmbedAllResponse
{
    public int ProjectsEmbedded { get; set; }
    public int TasksEmbedded { get; set; }
    public string Message { get; set; } = string.Empty;
}