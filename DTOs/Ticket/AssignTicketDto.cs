using System.ComponentModel.DataAnnotations;

namespace Task2_Internship.DTOs.Ticket
{
    public class AssignTicketDto
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required]
        public string AgentId { get; set; } = string.Empty;
    }
}