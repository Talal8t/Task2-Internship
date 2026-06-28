using Microsoft.AspNetCore.Identity;
using System.Net.Sockets;

namespace Task2_Internship.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public string ContactNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Ticket> CreatedTickets { get; set; }

        public ICollection<Ticket> AssignedTickets { get; set; }

        public ICollection<TicketReply> Replies { get; set; }
    }
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Agent = "Agent";
        public const string Customer = "Customer";
    }
}
