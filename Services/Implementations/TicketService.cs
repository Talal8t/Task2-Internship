using Microsoft.EntityFrameworkCore;
using Task2_Internship.Data;
using Task2_Internship.DTOs.Ticket;
using Task2_Internship.Entities;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        // Create Ticket
        public async Task<TicketDto> CreateAsync(string customerId, CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                CustomerId = customerId,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync();

            await _context.Entry(ticket)
                .Reference(t => t.Customer)
                .LoadAsync();

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                CustomerName = ticket.Customer.FullName,
                AssignedAgentName = null,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt
            };
        }

        // Get All Tickets
        public async Task<IEnumerable<TicketDto>> GetTicketsAsync(string userId, string role)
        {
            IQueryable<Ticket> query = _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.AssignedAgent);

            if (role == UserRoles.Customer)
            {
                query = query.Where(t => t.CustomerId == userId);
            }
            else if (role == UserRoles.Agent)
            {
                query = query.Where(t => t.AssignedAgentId == userId);
            }

            return await query.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CustomerName = t.Customer.FullName,
                AssignedAgentName = t.AssignedAgent != null
                    ? t.AssignedAgent.FullName
                    : null,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToListAsync();
        }

        // Get Ticket By Id
        public async Task<TicketDto?> GetByIdAsync(Guid id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.AssignedAgent)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
                return null;

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                CustomerName = ticket.Customer.FullName,
                AssignedAgentName = ticket.AssignedAgent?.FullName,
                CreatedAt = ticket.CreatedAt,
                UpdatedAt = ticket.UpdatedAt
            };
        }

        // Remaining methods will be added next...
        public async Task<bool> UpdateAsync(Guid id, UpdateTicketDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return false;

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AssignAsync(AssignTicketDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(dto.TicketId);

            if (ticket == null)
                return false;

            var agent = await _context.Users.FindAsync(dto.AgentId);

            if (agent == null)
                return false;

            ticket.AssignedAgentId = dto.AgentId;
            ticket.Status = TicketStatus.InProgress;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateStatusAsync(UpdateStatusDto dto)
        {
            var ticket = await _context.Tickets.FindAsync(dto.TicketId);

            if (ticket == null)
                return false;

            ticket.Status = dto.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<IEnumerable<TicketDto>> SearchAsync(
     string userId,
     string role,
     TicketFilterDto filter)
        {
            var query = _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.AssignedAgent)
                .AsQueryable();

            // Role-based filtering
            if (role == UserRoles.Customer)
            {
                query = query.Where(t => t.CustomerId == userId);
            }
            else if (role == UserRoles.Agent)
            {
                query = query.Where(t => t.AssignedAgentId == userId);
            }

            // Search filters
            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(t =>
                    t.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrWhiteSpace(filter.CustomerName))
            {
                query = query.Where(t =>
                    t.Customer.FullName.Contains(filter.CustomerName));
            }

            if (!string.IsNullOrWhiteSpace(filter.AssignedAgentId))
            {
                query = query.Where(t =>
                    t.AssignedAgentId == filter.AssignedAgentId);
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(t =>
                    t.Status == filter.Status.Value);
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(t =>
                    t.CreatedAt >= filter.FromDate.Value);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(t =>
                    t.CreatedAt <= filter.ToDate.Value);
            }
            if (!string.IsNullOrWhiteSpace(filter.AssignedAgentName))
            {
                query = query.Where(t =>
                    t.AssignedAgent != null &&
                    t.AssignedAgent.FullName.Contains(filter.AssignedAgentName));
            }

            return await query
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    CustomerName = t.Customer.FullName,
                    AssignedAgentName = t.AssignedAgent != null
                        ? t.AssignedAgent.FullName
                        : null,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                })
                .ToListAsync();
        }
    }
}