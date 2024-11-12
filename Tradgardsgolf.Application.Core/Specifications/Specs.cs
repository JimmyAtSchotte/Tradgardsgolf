using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications;

public class Specs
{
    public static SpecificationSet<Entities.Course> Course => new SpecificationSet<Entities.Course>();
    public static SpecificationSet<Entities.Scorecard> Scorecard => new SpecificationSet<Entities.Scorecard>();
    public static SpecificationSet<Entities.Tournament> Tournament => new SpecificationSet<Entities.Tournament>();
    
    public static SpecificationSet<Entities.PlayerStatistic> PlayerStatistic => new SpecificationSet<Entities.PlayerStatistic>();

    
    public static SpecificationSet<Entities.CourseSeason> CourseSeason => new SpecificationSet<Entities.CourseSeason>();

    
    public static ISpecification<T> ById<T>(Guid id) 
        where T : BaseEntity => new ById<T>(id);
    
    public static ISpecification<T> ById<T>(Guid id, Action<ISpecificationBuilder<T>> specification) 
        where T : BaseEntity => new ById<T>(id, specification);
}