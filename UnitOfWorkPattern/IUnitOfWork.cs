using GuardingChild.Models;
using GuardingChild.Repositories.Interfaces;

namespace GuardingChild.UnitOfWorkPattern;

public interface IUnitOfWork : IAsyncDisposable
{
    IGenericRepository<TModel> Repository<TModel>() where TModel : BaseModel;
    IKidRepository KidRepository { get; }
    Task<int> CompleteAsync();
}
