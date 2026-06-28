using System.ComponentModel.DataAnnotations;

namespace Task2_Internship.DTOs.Ticket
{
    public class CreateTicketDto
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}