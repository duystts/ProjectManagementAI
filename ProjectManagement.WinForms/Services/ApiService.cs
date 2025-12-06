using System.Text.Json;
using ProjectManagement.WinForms.Models;

namespace ProjectManagement.WinForms.Services
{
    public class ApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "http://localhost:5276/api";

        public ApiService()
        {
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

        public async Task<List<UserDto>> GetUsersAsync()
        {
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

    }
}