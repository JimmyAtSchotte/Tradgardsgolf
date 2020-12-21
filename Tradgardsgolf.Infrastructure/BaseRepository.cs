using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Tradgardsgolf.Infrastructure
{
    public abstract class BaseRepository
    {
        protected readonly TradgardsgolfContext db;

        protected BaseRepository(TradgardsgolfContext db)
        {
            this.db = db;
        }
    }
    
    
    public abstract class BaseRepository<TEntity> : RepositoryBase<TEntity>
        where TEntity : class
    {
        protected TradgardsgolfContext db;
        
        protected BaseRepository([NotNull] TradgardsgolfContext dbContext) : base(dbContext)
        {
            db = dbContext;
        }

        protected BaseRepository([NotNull] TradgardsgolfContext dbContext, [NotNull] [ItemNotNull] ISpecificationEvaluator<TEntity> specificationEvaluator) : base(dbContext, specificationEvaluator)
        {
            db = dbContext;
        }
    }
}
