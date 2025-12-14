using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using TaskStatusEnum = ProjectManagement.Entities.Models.Enums.TaskStatus;

namespace ProjectManagement.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<ProjectTaskDto>> GetTasksByProjectIdAsync(int projectId);
        Task<ProjectTaskDto?> GetTaskByIdAsync(int id);
        Task<ProjectTaskDto?> CreateTaskAsync(ProjectTaskDto task);
        Task<bool> UpdateTaskAsync(int id, ProjectTaskDto task);
        Task<bool> UpdateTaskStatusAsync(int id, TaskStatusEnum status);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> AssignTaskAsync(int taskId, int userId);
        Task<bool> SelfAssignTaskAsync(int taskId, int currentUserId);
        Task<bool> UnassignTaskAsync(int taskId);
    }
}