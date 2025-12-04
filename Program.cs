
using System.Threading.Tasks;
using GuardingChild.Data;
using GuardingChild.Helpers;
using GuardingChild.Repositories.Concretes;
using GuardingChild.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuardingChild
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<GuardingChildContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            #endregion

            var app = builder.Build();
            #region Update Database
            using var Scope = app.Services.CreateScope(); //group of services that lifetime scooped
            var Services = Scope.ServiceProvider; //catch services its self
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = Services.GetRequiredService<GuardingChildContext>(); //ASK CLR for Creating Object From DbContext Explicitly
                await dbContext.Database.MigrateAsync(); //Update - Database

                #region Data Seeding
                await GuardingChildSeed.SeedAsync(dbContext);
                #endregion
            }
            catch (Exception ex) {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured During Appling The Migration");
            }

            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "api");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
