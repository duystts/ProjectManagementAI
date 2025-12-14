using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.BLL.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(int id);
        Task<ProjectDto?> CreateProjectAsync(ProjectDto project);
        Task<bool> UpdateProjectAsync(int id, ProjectDto project);
        Task<bool> DeleteProjectAsync(int id);
    }
}