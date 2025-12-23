using ProjectManagement.Entities.Models.DTOs;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace ProjectManagement.BLL.Services;

public class EmbeddingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public EmbeddingService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GoogleAI:ApiKey"] ?? throw new InvalidOperationException("Google AI API key not found");
    }

    public async Task<EmbeddingResult> GetEmbeddingAsync(string text)
    {
        var request = new
        {
            model = "models/text-embedding-004",
            content = new { parts = new[] { new { text } } }
        };

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(
            $"https://generativelanguage.googleapis.com/v1/models/text-embedding-004:embedContent?key={_apiKey}",
            content);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                // Return dummy embedding when quota exceeded
                return new EmbeddingResult { Vector = new float[768] };
            }
            throw new Exception(errorContent);
        }
        
        var responseJson = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<GoogleEmbeddingResponse>(
            responseJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        
        return new EmbeddingResult
        {
            Vector = result?.Embedding?.Values ?? Array.Empty<float>()
        };
    }

    public float CosineSimilarity(float[] a, float[] b)
    {
        if (a.Length != b.Length) return 0;

        float dot = 0, normA = 0, normB = 0;
        for (int i = 0; i < a.Length; i++)
        {
            dot += a[i] * b[i];
            normA += a[i] * a[i];
            normB += b[i] * b[i];
        }

        return dot / (MathF.Sqrt(normA) * MathF.Sqrt(normB));
    }
}

public class GoogleEmbeddingResponse
{
    public EmbeddingData? Embedding { get; set; }
}

public class EmbeddingData
{
    public float[] Values { get; set; } = Array.Empty<float>();
}