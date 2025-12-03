using System.Text.Json;
using ProjectManagement.WinForms.Models;

namespace ProjectManagement.WinForms.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5276/api";

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

        public async Task<ProjectDto?> CreateProjectAsync(ProjectDto project)
        {
            try
            {
                var json = JsonSerializer.Serialize(project);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/projects", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<ProjectDto>(responseJson, options);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateProjectAsync(ProjectDto project)
        {
            try
            {
                var json = JsonSerializer.Serialize(project);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/projects/{project.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/projects/{projectId}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProjectTaskDto?> CreateTaskAsync(ProjectTaskDto task)
        {
            try
            {
                var request = new
                {
                    ProjectId = task.ProjectId,
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority
                };
                
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/tasks", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<ProjectTaskDto>(responseJson, options);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateTaskAsync(ProjectTaskDto task)
        {
            try
            {
                var request = new
                {
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority,
                    Status = task.Status
                };
                
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/tasks/{task.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/tasks/{taskId}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}