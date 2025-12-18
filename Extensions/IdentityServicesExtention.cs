using GuardingChild.Data;
using GuardingChild.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace GuardingChild.Extensions;

public static class IdentityServicesExtention
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
    {
        Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<GuardingChildContext>();
        Services.AddAuthentication();
        return Services;
    }
}