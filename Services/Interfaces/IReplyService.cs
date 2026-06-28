using Task2_Internship.DTOs.Reply;

namespace Task2_Internship.Services.Interfaces
{
    public interface IReplyService
    {
        Task<ReplyDto> AddReplyAsync(string senderId, string role, CreateReplyDto dto);
        Task<IEnumerable<ReplyDto>> GetRepliesAsync(
    Guid ticketId,
    string userId,
    string role);
    }
}