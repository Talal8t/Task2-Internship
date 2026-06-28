using Microsoft.AspNetCore.Identity;
using Task2_Internship.DTOs.Auth;
using Task2_Internship.Entities;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterCustomerAsync(RegisterDto dto)
        {
            return await RegisterAsync(dto, UserRoles.Customer);
        }

        public async Task<AuthResponseDto> RegisterAgentAsync(RegisterDto dto)
        {
            return await RegisterAsync(dto, UserRoles.Agent);
        }

        private async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, string role)
        {
            var exists = await _userManager.FindByEmailAsync(dto.Email);

            if (exists != null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Email already exists."
                };
            }

            var user = new ApplicationUser
            {
                FullName = dto.FullName,
                Email = dto.Email,
                UserName = dto.Email,
                ContactNumber = dto.ContactNumber,
                RegistrationDate = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            await _userManager.AddToRoleAsync(user, role);

            var token = await _jwtService.GenerateTokenAsync(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = $"{role} registered successfully.",
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!valid)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = "Invalid email or password."
                };
            }

            var token = await _jwtService.GenerateTokenAsync(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful.",
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}