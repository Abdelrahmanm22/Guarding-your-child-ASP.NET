using GuardingChild.Data;
using GuardingChild.Models;
using GuardingChild.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GuardingChild.Repositories.Concretes;

public class KidRepository : GenericRepository<Kid>, IKidRepository
{
    private readonly GuardingChildContext dbContext;

    public KidRepository(GuardingChildContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<long> GetLastIndexAsync()
    {
        return await dbContext.Set<Kid>()
            .OrderByDescending(k => k.Index)
            .Select(k => k.Index)
            .FirstOrDefaultAsync();
    }

    public async Task<Kid> GetByIndexAsync(long index)
    {
        return await dbContext.Set<Kid>()
            .Include(k => k.Guardian)
            .FirstOrDefaultAsync(k => k.Index == index);
    }
}
