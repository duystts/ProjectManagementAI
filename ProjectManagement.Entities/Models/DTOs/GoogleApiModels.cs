namespace ProjectManagement.Entities.Models.DTOs;

public class GoogleGenerateResponse
{
    public Candidate[]? Candidates { get; set; }
}

public class Candidate
{
    public Content? Content { get; set; }
}

public class Content
{
    public Part[]? Parts { get; set; }
}

public class Part
{
    public string Text { get; set; } = string.Empty;
}