using Task2_Internship.DTOs.Auth;

namespace Task2_Internship.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterCustomerAsync(RegisterDto dto);

        Task<AuthResponseDto> RegisterAgentAsync(RegisterDto dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}