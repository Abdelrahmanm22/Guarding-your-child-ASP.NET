using GuardingChild.Errors;
using GuardingChild.Helpers;
using GuardingChild.Repositories.Concretes;
using GuardingChild.Repositories.Interfaces;
using GuardingChild.UnitOfWorkPattern;
using Microsoft.AspNetCore.Mvc;

namespace GuardingChild.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(typeof(MappingProfiles));
        #region validation error
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(p=>p.Value.Errors.Count()>0)
                    .SelectMany(p=>p.Value.Errors)
                    .Select(p=>p.ErrorMessage)
                    .ToList();
                var ValidationErrorResponse = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(ValidationErrorResponse);
            });
        });
        #endregion
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}