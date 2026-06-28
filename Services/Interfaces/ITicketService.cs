using Task2_Internship.DTOs.Ticket;

namespace Task2_Internship.Services.Interfaces
{
    public interface ITicketService
    {
        Task<TicketDto> CreateAsync(string customerId, CreateTicketDto dto);

        Task<IEnumerable<TicketDto>> GetTicketsAsync(string userId, string role);

        Task<TicketDto?> GetByIdAsync(Guid id);

        Task<bool> UpdateAsync(Guid id, UpdateTicketDto dto);

        Task<bool> AssignAsync(AssignTicketDto dto);

        Task<bool> UpdateStatusAsync(UpdateStatusDto dto);
        Task<IEnumerable<TicketDto>> SearchAsync(
            string userId,
            string role,
            TicketFilterDto filter);
    }
}