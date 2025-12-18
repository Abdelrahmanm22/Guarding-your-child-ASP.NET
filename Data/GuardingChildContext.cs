using System.Reflection;
using GuardingChild.Models;
using GuardingChild.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GuardingChild.Data
{
    public class GuardingChildContext : IdentityDbContext<AppUser>
    {
        public GuardingChildContext(DbContextOptions<GuardingChildContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Kid> kids { get; set; }
        public DbSet<Guardian> guardians { get; set; }
    }
}
