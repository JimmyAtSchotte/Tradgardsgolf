using Ardalis.Specification;

namespace Tradgardsgolf.Core.Infrastructure;

public interface IRepository<T> : IRepositoryBase<T>
    where T : class { }