using GuardingChild.Data;
using GuardingChild.Models;
using GuardingChild.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuardingChild.Repositories.Concretes
{
    public class GenericRepository<T> : IGenericRepository<T> where T :BaseModel
    {
        private readonly GuardingChildContext dbContext;

        public GenericRepository(GuardingChildContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }
    }
}
