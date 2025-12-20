using GuardingChild.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace GuardingChild.Services.Interfaces;

public interface ITokenService
{
    //function to create token
    Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager);
}