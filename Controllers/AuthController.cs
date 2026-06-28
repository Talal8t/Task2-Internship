using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task2_Internship.DTOs.Auth;
using Task2_Internship.Entities;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // Customer Registration
        [HttpPost("register-customer")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterCustomer(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterCustomerAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Agent Registration (Admin Only)
        [HttpPost("register-agent")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterAgent(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAgentAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
                return Unauthorized(result);

            return Ok(result);
        }
    }
}