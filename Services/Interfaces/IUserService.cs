using Task2_Internship.DTOs.User;

namespace Task2_Internship.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetProfileAsync(string userId);

        Task<bool> UpdateProfileAsync(
            string userId,
            UpdateProfileDto dto);
    }
}