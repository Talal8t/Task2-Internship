using Task2_Internship.Entities;

namespace Task2_Internship.DTOs.Ticket
{
    public class TicketFilterDto
    {
        public string? Title { get; set; }

        public string? CustomerName { get; set; }

        public string? AssignedAgentId { get; set; }

        public TicketStatus? Status { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        public string? AssignedAgentName { get; set; }
    }
}