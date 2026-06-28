namespace Task2_Internship.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TicketStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string CustomerId { get; set; }

        public ApplicationUser Customer { get; set; }

        public string? AssignedAgentId { get; set; }

        public ApplicationUser? AssignedAgent { get; set; }

        public ICollection<TicketReply> Replies { get; set; }
    }
    public enum TicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }
}
