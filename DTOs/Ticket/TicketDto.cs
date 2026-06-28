using Task2_Internship.Entities;

namespace Task2_Internship.DTOs.Ticket
{
    public class TicketDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TicketStatus Status { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string? AssignedAgentName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}