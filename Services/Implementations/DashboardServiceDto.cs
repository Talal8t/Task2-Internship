using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task2_Internship.Data;
using Task2_Internship.DTOs.Dashboard;
using Task2_Internship.Entities;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardService(
            AppDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<DashboardDto> GetDashboardAsync()
        {
            var dashboard = new DashboardDto
            {
                TotalTickets = await _context.Tickets.CountAsync(),

                OpenTickets = await _context.Tickets
                    .CountAsync(t => t.Status == TicketStatus.Open),

                InProgressTickets = await _context.Tickets
                    .CountAsync(t => t.Status == TicketStatus.InProgress),

                ResolvedTickets = await _context.Tickets
                    .CountAsync(t => t.Status == TicketStatus.Resolved),

                ClosedTickets = await _context.Tickets
                    .CountAsync(t => t.Status == TicketStatus.Closed)
            };

            var users = await _userManager.Users.ToListAsync();

            int agents = 0;

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, UserRoles.Agent))
                    agents++;
            }

            dashboard.ActiveAgents = agents;

            return dashboard;
        }
    }
}