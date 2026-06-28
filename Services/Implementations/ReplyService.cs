using Microsoft.EntityFrameworkCore;
using Task2_Internship.Data;
using Task2_Internship.DTOs.Reply;
using Task2_Internship.Entities;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Services.Implementations
{
    public class ReplyService : IReplyService
    {
        private readonly AppDbContext _context;

        public ReplyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ReplyDto> AddReplyAsync(
    string senderId,
    string role,
    CreateReplyDto dto)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == dto.TicketId);

            if (ticket == null)
                throw new Exception("Ticket not found.");

            // Customer -> only own ticket
            if (role == UserRoles.Customer &&
                ticket.CustomerId != senderId)
            {
                throw new UnauthorizedAccessException(
                    "You are not allowed to reply to this ticket.");
            }

            // Agent -> only assigned ticket
            if (role == UserRoles.Agent &&
                ticket.AssignedAgentId != senderId)
            {
                throw new UnauthorizedAccessException(
                    "You are not assigned to this ticket.");
            }

            // Admin -> always allowed

            var reply = new TicketReply
            {
                Id = Guid.NewGuid(),
                TicketId = dto.TicketId,
                SenderId = senderId,
                Message = dto.Message,
                Timestamp = DateTime.UtcNow
            };

            _context.TicketReplies.Add(reply);

            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await _context.Entry(reply)
                .Reference(r => r.Sender)
                .LoadAsync();

            return new ReplyDto
            {
                Id = reply.Id,
                SenderName = reply.Sender.FullName,
                Message = reply.Message,
                Timestamp = reply.Timestamp
            };
        }

        public async Task<IEnumerable<ReplyDto>> GetRepliesAsync(
     Guid ticketId,
     string userId,
     string role)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
                throw new Exception("Ticket not found.");

            // Customer -> Only own ticket
            if (role == UserRoles.Customer &&
                ticket.CustomerId != userId)
            {
                throw new UnauthorizedAccessException(
                    "You are not authorized to view this conversation.");
            }

            // Agent -> Only assigned ticket
            if (role == UserRoles.Agent &&
                ticket.AssignedAgentId != userId)
            {
                throw new UnauthorizedAccessException(
                    "You are not assigned to this ticket.");
            }

            // Admin -> Allowed

            return await _context.TicketReplies
                .Where(r => r.TicketId == ticketId)
                .Include(r => r.Sender)
                .OrderBy(r => r.Timestamp)
                .Select(r => new ReplyDto
                {
                    Id = r.Id,
                    SenderName = r.Sender.FullName,
                    Message = r.Message,
                    Timestamp = r.Timestamp
                })
                .ToListAsync();
        }
    }
}