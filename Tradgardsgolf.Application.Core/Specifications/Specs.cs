using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications;

public class Specs
{
    public static SpecificationSet<Entities.Course> Course => new SpecificationSet<Entities.Course>();
    public static SpecificationSet<Entities.Scorecard> Scorecard => new SpecificationSet<Entities.Scorecard>();
    public static SpecificationSet<Entities.Tournament> Tournament => new SpecificationSet<Entities.Tournament>();
    
    public static ISpecification<T> ById<T>(Guid id) 
        where T : BaseEntity<T> => new ById<T>(id);
    
    public static ISpecification<T> ById<T>(Guid id, Action<ISpecificationBuilder<T>> specification) 
        where T : BaseEntity<T> => new ById<T>(id, specification);
}