using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.Data;
using ProjectManagement.DAL.Interfaces;
using ProjectManagement.Entities.Models;

namespace ProjectManagement.DAL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Project?> GetWithTasksAsync(int id)
        {
            return await _dbSet
                .Include(p => p.ProjectTasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}