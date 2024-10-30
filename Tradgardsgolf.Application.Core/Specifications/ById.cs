using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications;

public sealed class ById<T> : SpecificationEquatable<T, ById<T>> where T : BaseEntity
{
    public ById(Guid id) : base(id)
    {
        Query.Where(x => x.Id == id);
    }
    
    public ById(Guid id, Action<ISpecificationBuilder<T>> specification) : this(id)
    {
        specification.Invoke(Query);
    }
}