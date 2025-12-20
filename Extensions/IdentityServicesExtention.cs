using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using GuardingChild.Data;
using GuardingChild.Models.Identity;
using GuardingChild.Services.Concretes;
using GuardingChild.Services.Interfaces;


namespace GuardingChild.Extensions;

public static class IdentityServicesExtention
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration configuration)
    {
        Services.AddScoped<ITokenService,TokenService>();
        Services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<GuardingChildContext>();
        Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            }); //UserManager / SigninManager / RoleManager
        return Services;
    }
}