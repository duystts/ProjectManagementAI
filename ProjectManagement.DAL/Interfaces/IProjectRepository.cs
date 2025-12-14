using ProjectManagement.Entities.Models;

namespace ProjectManagement.DAL.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project?> GetWithTasksAsync(int id);
    }
}