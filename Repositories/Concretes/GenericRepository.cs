using GuardingChild.Data;
using GuardingChild.Models;
using GuardingChild.Repositories.Interfaces;
using GuardingChild.Specifications;
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
        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetByIdAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.BuildQuery(dbContext.Set<T>(), spec);
        }
    }
}
