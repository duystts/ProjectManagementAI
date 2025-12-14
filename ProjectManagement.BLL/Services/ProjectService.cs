using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL.Interfaces;
using ProjectManagement.Entities.Models;
using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            });
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return null;

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }

        public async Task<ProjectDto?> CreateProjectAsync(ProjectDto projectDto)
        {
            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                CreatedAt = DateTime.Now
            };

            var createdProject = await _projectRepository.AddAsync(project);
            
            return new ProjectDto
            {
                Id = createdProject.Id,
                Name = createdProject.Name,
                Description = createdProject.Description,
                CreatedAt = createdProject.CreatedAt
            };
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectDto projectDto)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return false;

            project.Name = projectDto.Name;
            project.Description = projectDto.Description;

            await _projectRepository.UpdateAsync(project);
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return false;

            await _projectRepository.DeleteAsync(project);
            return true;
        }
    }
}