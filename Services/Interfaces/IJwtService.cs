using Task2_Internship.Entities;

namespace Task2_Internship.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}