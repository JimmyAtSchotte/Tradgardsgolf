using Ardalis.Specification.EntityFrameworkCore;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure.Database
{
    public class Repository<TEntity> : BaseRepository<TEntity>
        where TEntity : class
    {
        public Repository(TradgardsgolfContext dbContext) : base(dbContext)
        {
        }
    }
    
    public class BaseRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        protected BaseRepository(TradgardsgolfContext dbContext) : base(dbContext)
        {
        }
    }
}
