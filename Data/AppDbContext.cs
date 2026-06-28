using Microsoft.EntityFrameworkCore;
using Task2_Internship.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Task2_Internship.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketReply> TicketReplies => Set<TicketReply>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.AssignedAgent)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssignedAgentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<TicketReply>()
                .HasOne(r => r.Ticket)
                .WithMany(t => t.Replies)
                .HasForeignKey(r => r.TicketId);

            builder.Entity<TicketReply>()
                .HasOne(r => r.Sender)
                .WithMany(u => u.Replies)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
