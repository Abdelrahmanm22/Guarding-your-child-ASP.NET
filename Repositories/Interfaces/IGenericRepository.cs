using GuardingChild.Models;
using GuardingChild.Specifications;

namespace GuardingChild.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
        Task AddAsync(T entity);
        void Update(T entity);
    }
}
