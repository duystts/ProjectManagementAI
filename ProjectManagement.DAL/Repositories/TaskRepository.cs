using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL.Data;
using ProjectManagement.DAL.Interfaces;
using ProjectManagement.Entities.Models;

namespace ProjectManagement.DAL.Repositories
{
    public class TaskRepository : GenericRepository<ProjectTask>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId)
        {
            return await _dbSet
                .Include(t => t.AssignedUser)
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProjectTask>> GetByAssignedUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(t => t.Project)
                .Where(t => t.AssignedUserId == userId)
                .ToListAsync();
        }
    }
}