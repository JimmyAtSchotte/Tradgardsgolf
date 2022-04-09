using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using JetBrains.Annotations;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure.Database
{
    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
        where TEntity : class
    {
        public Repository([NotNull] TradgardsgolfContext dbContext) : base(dbContext)
        {
        }
    }
}
