using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.API.Data;
using ProjectManagement.API.Models;
using ProjectManagement.API.Models.Enums;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTask>>> GetTasksByProject(int projectId)
        {
            return await _context.ProjectTasks
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ProjectTask>> CreateTask(CreateTaskRequest request)
        {
            var project = await _context.Projects.FindAsync(request.ProjectId);
            if (project == null)
                return NotFound("Project not found");

            var task = new ProjectTask
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = Models.Enums.TaskStatus.Todo,
                CreatedAt = DateTime.Now
            };

            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTasksByProject), new { projectId = task.ProjectId }, task);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, UpdateStatusRequest request)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null)
                return NotFound();

            task.Status = request.Status;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<ActionResult<TaskComment>> AddComment(int id, AddCommentRequest request)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null)
                return NotFound("Task not found");

            var comment = new TaskComment
            {
                ProjectTaskId = id,
                Content = request.Content,
                IsAiGenerated = request.IsAiGenerated,
                CreatedAt = DateTime.Now
            };

            _context.TaskComments.Add(comment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddComment), new { id = comment.Id }, comment);
        }
    }

    public class CreateTaskRequest
    {
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskPriority Priority { get; set; }
    }

    public class UpdateStatusRequest
    {
        public Models.Enums.TaskStatus Status { get; set; }
    }

    public class AddCommentRequest
    {
        public string Content { get; set; } = string.Empty;
        public bool IsAiGenerated { get; set; }
    }
}