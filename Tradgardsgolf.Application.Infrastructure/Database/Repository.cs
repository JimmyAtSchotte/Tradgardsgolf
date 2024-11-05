using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure.Database;

public class Repository : IRepository
{
    private readonly TradgardsgolfContext _context;
    private readonly SpecificationEvaluator _specificationEvaluator = SpecificationEvaluator.Default;


    public Repository(TradgardsgolfContext context)
    {
        _context = context;
    }

    public async Task<TEntity> FirstOrDefaultAsync<TEntity>(ISpecification<TEntity> specification, CancellationToken cancellationToken) 
        where TEntity : class
    {
        return await _specificationEvaluator.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> ListAsync<TEntity>(CancellationToken cancellationToken)
        where TEntity : class
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> ListAsync<TEntity>(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        where TEntity : class
    {
        var queryResult = await _specificationEvaluator.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).ToListAsync(cancellationToken);
        
        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : class
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
    
    public async Task<TEntity[]> UpdateRangeAsync<TEntity>(TEntity[] entitites, CancellationToken cancellationToken)
        where TEntity : class
    {
        foreach (var entity in entitites)
            _context.Set<TEntity>().Update(entity);
        
        await _context.SaveChangesAsync(cancellationToken);

        return entitites;
    }
}
    

