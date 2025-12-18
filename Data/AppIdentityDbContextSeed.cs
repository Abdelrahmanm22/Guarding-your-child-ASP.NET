using GuardingChild.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace GuardingChild.Data;

public static class AppIdentityDbContextSeed
{
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Doctor 1",
                Email = "abdelrahmanmohamed2293@gmail.com",
                UserName = "Doctor1",
                PhoneNumber = "01015496488",
            };
            await userManager.CreateAsync(user, "12345678");
            
            var user2 = new AppUser
            {
                DisplayName = "Police 1",
                Email = "abdra1396@gmail.com",
                UserName = "Police1",
                PhoneNumber = "01015496488",
            };
            await userManager.CreateAsync(user2, "12345678");
        }
    }
}