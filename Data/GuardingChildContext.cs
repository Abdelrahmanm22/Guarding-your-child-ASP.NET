using System.Reflection;
using GuardingChild.Models;
using Microsoft.EntityFrameworkCore;

namespace GuardingChild.Data
{
    public class GuardingChildContext : DbContext
    {
        public GuardingChildContext(DbContextOptions<GuardingChildContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Kid> kids { get; set; }
        public DbSet<Guardian> guardians { get; set; }
    }
}
