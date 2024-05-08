using Ardalis.Specification.EntityFrameworkCore;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure.Database;

public class Repository<TEntity>(TradgardsgolfContext dbContext) : BaseRepository<TEntity>(dbContext)
    where TEntity : class;

public class BaseRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : class
{
    protected BaseRepository(TradgardsgolfContext dbContext) : base(dbContext) { }
}