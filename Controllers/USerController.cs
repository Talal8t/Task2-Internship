using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task2_Internship.DTOs.User;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class USerController : ControllerBase
    {
        private readonly IUserService _userService;

        public USerController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var profile = await _userService.GetProfileAsync(userId);

            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(
            UpdateProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var updated = await _userService.UpdateProfileAsync(
                userId,
                dto);

            if (!updated)
                return BadRequest();

            return NoContent();
        }
    }
}