using System;
using System.Linq;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications;

public abstract class SpecificationEquatable<TEntity, TSpecification> : Specification<TEntity>, IEquatable<SpecificationEquatable<TEntity, TSpecification>>
 {
    private readonly object[] _args;
    protected SpecificationEquatable(params object[] args)
    {
        _args = args;
    }
    
    public bool Equals(SpecificationEquatable<TEntity, TSpecification> other)
    {
        return other != null && _args.SequenceEqual(other._args);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        
        return obj.GetType() == GetType() && 
               Equals((SpecificationEquatable<TEntity, TSpecification>)obj);
    }

    public override int GetHashCode()
    {
        if (_args is null) return 0;
        
        var hash = new HashCode();
        hash.Add(GetType());

        foreach (var arg in _args)
            hash.Add(arg);
        
        return hash.ToHashCode();
    }
}