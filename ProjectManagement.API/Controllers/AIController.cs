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

        [HttpPost("embed-all")]
        public async Task<ActionResult<EmbedAllResponse>> EmbedAllData()
        {
            var result = await _ragService.EmbedAllDataAsync();
            return Ok(result);
        }
    }

    public class AddKnowledgeRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int? ProjectId { get; set; }
        public int? TaskId { get; set; }
    }

    public class EmbedAllResponse
    {
        public int ProjectsEmbedded { get; set; }
        public int TasksEmbedded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}