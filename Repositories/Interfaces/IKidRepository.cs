using GuardingChild.Models;

namespace GuardingChild.Repositories.Interfaces;

public interface IKidRepository : IGenericRepository<Kid>
{
    Task<long> GetLastIndexAsync();
    Task<Kid> GetByIndexAsync(long index);
}
