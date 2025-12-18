using Microsoft.AspNetCore.Identity;

namespace GuardingChild.Models.Identity;

public class AppUser : IdentityUser
{
    public string DisplayName { get; set; }
}