using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task2_Internship.DTOs.Reply;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RepliesController : ControllerBase
    {
        private readonly IReplyService _replyService;

        public RepliesController(IReplyService replyService)
        {
            _replyService = replyService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(CreateReplyDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var role = User.FindFirstValue(ClaimTypes.Role)!;

            var reply = await _replyService.AddReplyAsync(userId, role, dto);

            return Ok(reply);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetReplies(Guid ticketId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var role = User.FindFirstValue(ClaimTypes.Role)!;

            var replies = await _replyService.GetRepliesAsync(
                ticketId,
                userId,
                role);

            return Ok(replies);
        }
    }
}