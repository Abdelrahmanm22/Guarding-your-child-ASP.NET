using GuardingChild.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace GuardingChild.Data;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(UserRoles.Doctor))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Doctor));
        }

        if (!await roleManager.RoleExistsAsync(UserRoles.Police))
        {
            await roleManager.CreateAsync(new IdentityRole(UserRoles.Police));
        }

        if (!userManager.Users.Any())
        {
            var doctor = new AppUser
            {
                DisplayName = "Doctor 1",
                Email = "abdelrahmanmohamed2293@gmail.com",
                UserName = "Doctor1",
                PhoneNumber = "01015496488",
            };
            await userManager.CreateAsync(doctor, "Pa$$w0rd");
            await userManager.AddToRoleAsync(doctor, UserRoles.Doctor);
            
            var police = new AppUser
            {
                DisplayName = "Police 1",
                Email = "abdra1396@gmail.com",
                UserName = "Police1",
                PhoneNumber = "01015496488",
            };
            await userManager.CreateAsync(police, "Pa$$w0rd");
            await userManager.AddToRoleAsync(police, UserRoles.Police);
        }
    }
}
