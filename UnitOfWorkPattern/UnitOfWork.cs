using System.Collections;
using GuardingChild.Data;
using GuardingChild.Models;
using GuardingChild.Repositories.Concretes;
using GuardingChild.Repositories.Interfaces;

namespace GuardingChild.UnitOfWorkPattern;

public class UnitOfWork : IUnitOfWork
{
    private readonly GuardingChildContext _dbContext;
    private IKidRepository _kidRepository;
    //private Dictionary<string, GenericRepository<>> _repositories; this need casting
    private Hashtable _repositories;

    public UnitOfWork(GuardingChildContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Hashtable();
    }
    public ValueTask DisposeAsync()
    {
        return _dbContext.DisposeAsync();
    }

    public IGenericRepository<TModel> Repository<TModel>() where TModel : BaseModel
    {
        var type = typeof(TModel).Name; //Kid
        if (!_repositories.ContainsKey(type)) {
            //first time
            var Repo = new GenericRepository<TModel>(_dbContext);
            _repositories.Add(type, Repo);
        }
        return _repositories[type] as IGenericRepository<TModel>;
    }

    public IKidRepository KidRepository => _kidRepository ??= new KidRepository(_dbContext);

    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
