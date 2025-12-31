using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL.Interfaces;
using ProjectManagement.Entities.Models;
using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using TaskStatusEnum = ProjectManagement.Entities.Models.Enums.TaskStatus;

namespace ProjectManagement.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly RagService _ragService;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository, IProjectRepository projectRepository, RagService ragService)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _ragService = ragService;
        }

        public async Task<IEnumerable<ProjectTaskDto>> GetTasksByProjectIdAsync(int projectId)
        {
            var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
            return tasks.Select(t => new ProjectTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Priority = t.Priority,
                Deadline = t.Deadline,
                CreatedAt = t.CreatedAt,
                ProjectId = t.ProjectId,
                AssignedUserId = t.AssignedUserId,
                AssignedUserName = t.AssignedUser?.Username
            });
        }

        public async Task<ProjectTaskDto?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return new ProjectTaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                Priority = task.Priority,
                Deadline = task.Deadline,
                CreatedAt = task.CreatedAt,
                ProjectId = task.ProjectId,
                AssignedUserId = task.AssignedUserId,
                AssignedUserName = task.AssignedUser?.Username
            };
        }

        public async Task<ProjectTaskDto?> CreateTaskAsync(ProjectTaskDto taskDto)
        {
            if (!await _projectRepository.ExistsAsync(taskDto.ProjectId))
                return null;

            var task = new ProjectTask
            {
                ProjectId = taskDto.ProjectId,
                Title = taskDto.Title,
                Description = taskDto.Description,
                Priority = taskDto.Priority,
                Deadline = taskDto.Deadline,
                Status = TaskStatusEnum.Todo,
                CreatedAt = DateTime.Now
            };

            var createdTask = await _taskRepository.AddAsync(task);
            
            // Auto-embed new task
            try
            {
                var project = await _projectRepository.GetByIdAsync(taskDto.ProjectId);
                var taskContent = $"Task: {createdTask.Title}\nDescription: {createdTask.Description}\nStatus: {createdTask.Status}\nPriority: {createdTask.Priority}\nCreated: {createdTask.CreatedAt}\nProject: {project?.Name}";
                await _ragService.AddKnowledgeAsync($"Task: {createdTask.Title}", taskContent, createdTask.ProjectId, createdTask.Id);
            }
            catch { /* Ignore embedding errors */ }
            
            return new ProjectTaskDto
            {
                Id = createdTask.Id,
                Title = createdTask.Title,
                Description = createdTask.Description,
                Status = createdTask.Status,
                Priority = createdTask.Priority,
                Deadline = createdTask.Deadline,
                CreatedAt = createdTask.CreatedAt,
                ProjectId = createdTask.ProjectId
            };
        }

        public async Task<bool> UpdateTaskAsync(int id, ProjectTaskDto taskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.Priority = taskDto.Priority;
            task.Deadline = taskDto.Deadline;
            task.Status = taskDto.Status;

            await _taskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> UpdateTaskStatusAsync(int id, TaskStatusEnum status)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            task.Status = status;
            await _taskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            await _taskRepository.DeleteAsync(task);
            return true;
        }

        public async Task<bool> AssignTaskAsync(int taskId, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) return false;

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return false;

            task.AssignedUserId = userId;
            await _taskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> SelfAssignTaskAsync(int taskId, int currentUserId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) return false;

            task.AssignedUserId = currentUserId;
            await _taskRepository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> UnassignTaskAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null) return false;

            task.AssignedUserId = null;
            await _taskRepository.UpdateAsync(task);
            return true;
        }
    }
}