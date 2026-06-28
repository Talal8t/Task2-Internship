namespace Task2_Internship.DTOs.Reply
{
    public class ReplyDto
    {
        public Guid Id { get; set; }

        public string SenderName { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }
    }
}