using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL.Data;
using ProjectManagement.Entities.Models;
using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.BLL.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly AppDbContext _context;
        private readonly string _uploadPath;

        public AttachmentService(AppDbContext context)
        {
            _context = context;
            _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            Directory.CreateDirectory(_uploadPath);
        }

        public async Task<TaskAttachmentDto?> UploadAttachmentAsync(int taskId, IFormFile file, int userId)
        {
            // Validate task exists
            var task = await _context.ProjectTasks.FindAsync(taskId);
            if (task == null) return null;

            // Validate file type
            var allowedTypes = new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".avi", ".mov", ".wmv" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedTypes.Contains(extension))
                return null;

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Determine file type
            var fileType = new[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(extension) ? "image" : "video";

            // Save to database
            var attachment = new TaskAttachment
            {
                FileName = file.FileName,
                FilePath = fileName,
                FileType = fileType,
                FileSize = file.Length,
                TaskId = taskId,
                UploadedByUserId = userId,
                UploadedAt = DateTime.Now
            };

            _context.TaskAttachments.Add(attachment);
            await _context.SaveChangesAsync();

            return new TaskAttachmentDto
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FilePath = attachment.FilePath,
                FileType = attachment.FileType,
                FileSize = attachment.FileSize,
                TaskId = attachment.TaskId,
                UploadedByUserId = attachment.UploadedByUserId,
                UploadedAt = attachment.UploadedAt
            };
        }

        public async Task<IEnumerable<TaskAttachmentDto>> GetTaskAttachmentsAsync(int taskId)
        {
            return await _context.TaskAttachments
                .Include(a => a.UploadedBy)
                .Where(a => a.TaskId == taskId)
                .Select(a => new TaskAttachmentDto
                {
                    Id = a.Id,
                    FileName = a.FileName,
                    FilePath = a.FilePath,
                    FileType = a.FileType,
                    FileSize = a.FileSize,
                    TaskId = a.TaskId,
                    UploadedByUserId = a.UploadedByUserId,
                    UploadedByUserName = a.UploadedBy.FullName,
                    UploadedAt = a.UploadedAt
                })
                .ToListAsync();
        }

        public async Task<TaskAttachmentDto?> GetAttachmentAsync(int id)
        {
            var attachment = await _context.TaskAttachments
                .Include(a => a.UploadedBy)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (attachment == null) return null;

            return new TaskAttachmentDto
            {
                Id = attachment.Id,
                FileName = attachment.FileName,
                FilePath = attachment.FilePath,
                FileType = attachment.FileType,
                FileSize = attachment.FileSize,
                TaskId = attachment.TaskId,
                UploadedByUserId = attachment.UploadedByUserId,
                UploadedByUserName = attachment.UploadedBy.FullName,
                UploadedAt = attachment.UploadedAt
            };
        }

        public async Task<bool> DeleteAttachmentAsync(int id, int userId)
        {
            var attachment = await _context.TaskAttachments.FindAsync(id);
            if (attachment == null || attachment.UploadedByUserId != userId)
                return false;

            // Delete file
            var filePath = Path.Combine(_uploadPath, attachment.FilePath);
            if (File.Exists(filePath))
                File.Delete(filePath);

            // Delete from database
            _context.TaskAttachments.Remove(attachment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}