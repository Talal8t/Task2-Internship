using System.ComponentModel.DataAnnotations;

namespace Task2_Internship.DTOs.Reply
{
    public class CreateReplyDto
    {
        [Required]
        public Guid TicketId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;
    }
}