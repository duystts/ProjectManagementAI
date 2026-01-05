using Microsoft.AspNetCore.Http;
using ProjectManagement.Entities.Models.DTOs;

namespace ProjectManagement.BLL.Interfaces
{
    public interface IAttachmentService
    {
        Task<TaskAttachmentDto?> UploadAttachmentAsync(int taskId, IFormFile file, int userId);
        Task<IEnumerable<TaskAttachmentDto>> GetTaskAttachmentsAsync(int taskId);
        Task<TaskAttachmentDto?> GetAttachmentAsync(int id);
        Task<bool> DeleteAttachmentAsync(int id, int userId);
    }
}