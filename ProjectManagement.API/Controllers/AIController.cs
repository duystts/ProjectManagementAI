using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectManagement.Entities.Models.DTOs;
using ProjectManagement.BLL.Services;

namespace ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AIController : ControllerBase
    {
        private readonly RagService _ragService;

        public AIController(RagService ragService)
        {
            _ragService = ragService;
        }

        [HttpPost("chat")]
        public async Task<ActionResult<ChatResponse>> Chat([FromBody] ChatRequest request)
        {
            var response = await _ragService.ChatAsync(request);
            return Ok(response);
        }

        [HttpPost("knowledge")]
        public async Task<ActionResult<int>> AddKnowledge([FromBody] AddKnowledgeRequest request)
        {
            var id = await _ragService.AddKnowledgeAsync(request.Title, request.Content, request.ProjectId, request.TaskId);
            return Ok(id);
        }
    }

    public class AddKnowledgeRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }
    }
}