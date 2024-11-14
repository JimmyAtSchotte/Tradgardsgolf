using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Infrastructure;

public interface IRepository
{
    Task<TEntity> FirstOrDefaultAsync<TEntity>(ISpecification<TEntity> specification, CancellationToken cancellationToken) where TEntity : class;
    
    Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

    
    Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class;

    Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(TEntity[] entitites, CancellationToken cancellationToken)
        where TEntity : class;
    
    Task<IEnumerable<TEntity>> ListAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;
    Task<IEnumerable<TEntity>> ListAsync<TEntity>(ISpecification<TEntity> specification, CancellationToken cancellationToken) where TEntity : class;
    Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(TEntity[] entities, CancellationToken cancellationToken) where TEntity : class;
}