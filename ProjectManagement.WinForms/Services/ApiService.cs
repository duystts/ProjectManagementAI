using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.WinForms.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiBaseUrl"] ?? "http://localhost:5276/api";
            UpdateAuthHeader();
        }

        private void UpdateAuthHeader()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrEmpty(AuthService.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.Token);
            }
        }


        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/auth/login", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<AuthResponse>(responseJson, options);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            UpdateAuthHeader();
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
            UpdateAuthHeader();
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
            UpdateAuthHeader();
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
            UpdateAuthHeader();
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
            UpdateAuthHeader();
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
            UpdateAuthHeader();
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
            UpdateAuthHeader();
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
            UpdateAuthHeader();
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

        public async Task<List<UserDto>> GetUsersAsync()
        {
            UpdateAuthHeader();
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/users");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<List<UserDto>>(json, options) ?? new List<UserDto>();
                }
                return new List<UserDto>();
            }
            catch
            {
                return new List<UserDto>();
            }
        }

        public async Task<UserDto?> CreateUserAsync(CreateUserRequest request)
        {
            UpdateAuthHeader();
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/users", content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<UserDto>(responseJson, options);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateUserAsync(int userId, UpdateUserRequest request)
        {
            UpdateAuthHeader();
            try
            {
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseUrl}/users/{userId}", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            UpdateAuthHeader();
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/users/{userId}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
        {
            UpdateAuthHeader();
            try
            {
                var request = new { NewPassword = newPassword };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}/users/{userId}/reset-password", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AssignTaskAsync(int taskId, int userId)
        {
            UpdateAuthHeader();
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/tasks/{taskId}/assign/{userId}", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SelfAssignTaskAsync(int taskId)
        {
            UpdateAuthHeader();
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/tasks/{taskId}/assign-self", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnassignTaskAsync(int taskId)
        {
            UpdateAuthHeader();
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/tasks/{taskId}/unassign", null);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ChatResponse> ChatAsync(ChatRequest request)
        {
            UpdateAuthHeader();
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/ai/chat", content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<ChatResponse>(responseJson, options)!;
        }

        public async Task<int> AddKnowledgeAsync(string title, string content, int? projectId, int? taskId = null)
        {
            UpdateAuthHeader();
            var request = new { Title = title, Content = content, ProjectId = projectId, TaskId = taskId };
            var json = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/ai/knowledge", httpContent);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(responseJson);
        }

        public async Task<string> SendChatMessageAsync(string message)
        {
            try
            {
                var request = new ChatRequest { Message = message };
                var response = await ChatAsync(request);
                return response.Response;
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> EmbedAllDataAsync()
        {
            UpdateAuthHeader();
            try
            {
                var response = await _httpClient.PostAsync($"{_baseUrl}/ai/embed-all", null);
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<EmbedAllResponse>(responseJson, options);
                    return result?.Message ?? "Embedding completed";
                }
                return "Failed to embed data";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }
}