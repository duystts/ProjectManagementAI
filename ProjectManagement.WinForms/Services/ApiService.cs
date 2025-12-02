using System.Text.Json;
using ProjectManagement.WinForms.Models;

namespace ProjectManagement.WinForms.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7089/api";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/projects");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<List<ProjectDto>>(json, options) ?? new List<ProjectDto>();
                }
                return new List<ProjectDto>();
            }
            catch
            {
                return new List<ProjectDto>();
            }
        }

        public async Task<List<ProjectTaskDto>> GetTasksByProjectAsync(int projectId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/tasks/project/{projectId}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<List<ProjectTaskDto>>(json, options) ?? new List<ProjectTaskDto>();
                }
                return new List<ProjectTaskDto>();
            }
            catch
            {
                return new List<ProjectTaskDto>();
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}