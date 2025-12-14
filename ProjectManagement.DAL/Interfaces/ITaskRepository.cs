using ProjectManagement.Entities.Models;

namespace ProjectManagement.DAL.Interfaces
{
    public interface ITaskRepository : IGenericRepository<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId);
        Task<IEnumerable<ProjectTask>> GetByAssignedUserIdAsync(int userId);
    }
}