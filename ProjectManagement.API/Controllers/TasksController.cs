using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.Entities.Models.Enums;
using TaskStatusEnum = ProjectManagement.Entities.Models.Enums.TaskStatus;
using System.Security.Claims;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ProjectTaskDto>>> GetTasksByProject(int projectId)
        {
            var tasks = await _taskService.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Member")]
        public async Task<ActionResult<ProjectTaskDto>> CreateTask(CreateTaskRequest request)
        {
            var taskDto = new ProjectTaskDto
            {
                ProjectId = request.ProjectId,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority
            };

            var result = await _taskService.CreateTaskAsync(taskDto);
            if (result == null)
                return NotFound("Project not found");

            return CreatedAtAction(nameof(GetTasksByProject), new { projectId = result.ProjectId }, result);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,Manager,Member")]
        public async Task<IActionResult> UpdateTaskStatus(int id, UpdateStatusRequest request)
        {
            var success = await _taskService.UpdateTaskStatusAsync(id, request.Status);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager,Member")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskRequest request)
        {
            var taskDto = new ProjectTaskDto
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Priority = request.Priority,
                Status = request.Status
            };

            var success = await _taskService.UpdateTaskAsync(id, taskDto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Manager,Member")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/assign/{userId}")]
        [Authorize(Roles = "Admin,Manager,Member")]
        public async Task<IActionResult> AssignTask(int id, int userId)
        {
            var success = await _taskService.AssignTaskAsync(id, userId);
            if (!success)
                return NotFound("Task or User not found");

            return NoContent();
        }

        [HttpPost("{id}/assign-self")]
        [Authorize(Roles = "Manager,Member")]
        public async Task<IActionResult> SelfAssignTask(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int currentUserId))
                return Unauthorized();

            var success = await _taskService.SelfAssignTaskAsync(id, currentUserId);
            if (!success)
                return NotFound("Task not found");

            return NoContent();
        }

        [HttpPost("{id}/unassign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnassignTask(int id)
        {
            var success = await _taskService.UnassignTaskAsync(id);
            if (!success)
                return NotFound("Task not found");

            return NoContent();
        }

        // TODO: Implement comment functionality in BLL layer
    }


}