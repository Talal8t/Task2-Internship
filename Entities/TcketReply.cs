namespace Task2_Internship.Entities
{
    public class TicketReply
    {
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
