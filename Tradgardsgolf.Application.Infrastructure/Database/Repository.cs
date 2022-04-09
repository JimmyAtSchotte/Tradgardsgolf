using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using JetBrains.Annotations;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure.Database
{
    public class Repository<TEntity> : BaseRepository<TEntity>
        where TEntity : class
    {
        public Repository([NotNull] TradgardsgolfContext dbContext) : base(dbContext)
        {
        }
    }
    
    public class BaseRepository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        protected BaseRepository([NotNull] TradgardsgolfContext dbContext) : base(dbContext)
        {
        }
    }
}
