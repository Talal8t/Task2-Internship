using Microsoft.AspNetCore.Identity;
using Task2_Internship.DTOs.User;
using Task2_Internship.Entities;
using Task2_Internship.Services.Interfaces;

namespace Task2_Internship.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserProfileDto?> GetProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                ContactNumber = user.ContactNumber,
                RegistrationDate = user.RegistrationDate,
                Role = roles.FirstOrDefault() ?? ""
            };
        }

        public async Task<bool> UpdateProfileAsync(
            string userId,
            UpdateProfileDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            user.FullName = dto.FullName;
            user.ContactNumber = dto.ContactNumber;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }
    }
}