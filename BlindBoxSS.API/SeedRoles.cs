using Microsoft.AspNetCore.Identity;
using Models;

namespace BlindBoxSS.API
{
    public class SeedRoles
    {
        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "User", "Manager" };

            // Tạo role nếu chưa tồn tại
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            // Tạo tài khoản Admin mặc định nếu chưa có
            var adminEmail = "blindboxsaleswebsite@gmail.com";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Mystic BlindBox",
                    Email = adminEmail,
                    FirstName = "Mystic",
                    LastName = "BlindBox",
                    Gender = "Male",
                    PhoneNumber = "0123456789",
                    EmailConfirmed = true,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
