using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.Data;
using ProjectManagement.Entities.Models;
using ProjectManagement.Entities.Models.DTOs;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ProjectManagement.BLL.Services;

public class RagService
{
    private readonly AppDbContext _context;
    private readonly EmbeddingService _embeddingService;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public RagService(AppDbContext context, EmbeddingService embeddingService, HttpClient httpClient, IConfiguration configuration)
    {
        _context = context;
        _embeddingService = embeddingService;
        _httpClient = httpClient;
        _apiKey = configuration["GoogleAI:ApiKey"] ?? throw new InvalidOperationException("Google AI API key not found");
    }

    public async Task<int> AddKnowledgeAsync(string title, string content, int? projectId = null, int? taskId = null)
    {
        var embedding = await _embeddingService.GetEmbeddingAsync(content);
        
        var knowledge = new Knowledge
        {
            Title = title,
            Content = content,
            Embedding = JsonSerializer.Serialize(embedding.Vector),
            ProjectId = projectId,
            TaskId = taskId
        };

        _context.Knowledge.Add(knowledge);
        await _context.SaveChangesAsync();
        return knowledge.Id;
    }

    public async Task<ChatResponse> ChatAsync(ChatRequest request)
    {
        var queryEmbedding = await _embeddingService.GetEmbeddingAsync(request.Message);
        var relevantKnowledge = await FindRelevantKnowledgeAsync(queryEmbedding.Vector, request.ProjectId, request.TaskId);
        
        var context = string.Join("\n\n", relevantKnowledge.Select(k => $"Title: {k.Title}\nContent: {k.Content}"));
        var prompt = $"Context:\n{context}\n\nQuestion: {request.Message}\n\nAnswer based on the context above:";

        var apiRequest = new
        {
            contents = new[] { new { parts = new[] { new { text = prompt } } } }
        };

        var json = JsonSerializer.Serialize(apiRequest);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}",
            content);
        
        var responseJson = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GoogleGenerateResponse>(responseJson);

        return new ChatResponse
        {
            Response = result?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text ?? "No response",
            Sources = relevantKnowledge.Select(k => k.Title).ToList()
        };
    }

    private async Task<List<Knowledge>> FindRelevantKnowledgeAsync(float[] queryVector, int? projectId = null, int? taskId = null, int topK = 5)
    {
        var query = _context.Knowledge.AsQueryable();
        
        if (projectId.HasValue)
            query = query.Where(k => k.ProjectId == projectId);
        if (taskId.HasValue)
            query = query.Where(k => k.TaskId == taskId);

        var allKnowledge = await query.ToListAsync();
        
        var similarities = allKnowledge
            .Select(k => new
            {
                Knowledge = k,
                Similarity = _embeddingService.CosineSimilarity(queryVector, JsonSerializer.Deserialize<float[]>(k.Embedding)!)
            })
            .OrderByDescending(x => x.Similarity)
            .Take(topK)
            .Where(x => x.Similarity > 0.3f)
            .Select(x => x.Knowledge)
            .ToList();

        return similarities;
    }
}