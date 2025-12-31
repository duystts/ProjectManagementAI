using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.Entities.Models.DTOs;
using System.Security.Claims;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/attachments")]
    [Authorize]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<TaskAttachmentDto>> UploadAttachment([FromForm] int taskId, [FromForm] IFormFile file)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var result = await _attachmentService.UploadAttachmentAsync(taskId, file, userId);
            if (result == null)
                return BadRequest("Failed to upload attachment");

            return Ok(result);
        }

        [HttpGet("task/{taskId}")]
        public async Task<ActionResult<IEnumerable<TaskAttachmentDto>>> GetTaskAttachments(int taskId)
        {
            var attachments = await _attachmentService.GetTaskAttachmentsAsync(taskId);
            return Ok(attachments);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var success = await _attachmentService.DeleteAttachmentAsync(id, userId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadAttachment(int id)
        {
            var attachment = await _attachmentService.GetAttachmentAsync(id);
            if (attachment == null)
                return NotFound();

            var filePath = Path.Combine("uploads", attachment.FilePath);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(fileBytes, "application/octet-stream", attachment.FileName);
        }
    }
}