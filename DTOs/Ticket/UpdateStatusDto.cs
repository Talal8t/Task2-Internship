using System.ComponentModel.DataAnnotations;
using Task2_Internship.Entities;


namespace Task2_Internship.DTOs.Ticket
{
    public class UpdateStatusDto
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required]
        public TicketStatus Status { get; set; }
    }
}