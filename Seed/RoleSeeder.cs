using Microsoft.AspNetCore.Identity;
using Task2_Internship.Entities;

namespace Task2_Internship.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            string[] roles =
            {
                UserRoles.Admin,
                UserRoles.Agent,
                UserRoles.Customer
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var adminEmail = "admin@support.com";

            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    FullName = "System Administrator",
                    Email = adminEmail,
                    UserName = adminEmail,
                    ContactNumber = "0000000000",
                    RegistrationDate = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);
                }
            }
        }
    }
}
