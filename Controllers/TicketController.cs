using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Task2_Internship.DTOs.Ticket;
using Task2_Internship.Entities;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Customer)]
        public async Task<IActionResult> Create(CreateTicketDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var ticket = await _ticketService.CreateAsync(userId, dto);

            return Ok(ticket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var role = User.FindFirstValue(ClaimTypes.Role)!;

            var tickets = await _ticketService.GetTicketsAsync(userId, role);

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ticket = await _ticketService.GetByIdAsync(id);

            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Customer + "," + UserRoles.Admin)]
        public async Task<IActionResult> Update(Guid id, UpdateTicketDto dto)
        {
            var result = await _ticketService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("assign")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Assign(AssignTicketDto dto)
        {
            var result = await _ticketService.AssignAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("status")]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Agent)]
        public async Task<IActionResult> UpdateStatus(UpdateStatusDto dto)
        {
            var result = await _ticketService.UpdateStatusAsync(dto);

            if (!result)
                return NotFound();

            return NoContent();
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] TicketFilterDto filter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var role = User.FindFirstValue(ClaimTypes.Role)!;

            var tickets = await _ticketService.SearchAsync(
                userId,
                role,
                filter);

            return Ok(tickets);
        }
    }
}