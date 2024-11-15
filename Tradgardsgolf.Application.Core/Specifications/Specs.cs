using System;
using System.Diagnostics.CodeAnalysis;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications;

public class Specs
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")] 
    public static SpecificationSet<Course> Course => new();
    public static SpecificationSet<Entities.Scorecard> Scorecard => new();
    public static SpecificationSet<Entities.Tournament> Tournament => new();
    
    public static SpecificationSet<Entities.PlayerStatistic> PlayerStatistic => new();

    
    public static SpecificationSet<Entities.CourseSeason> CourseSeason => new();

    
    public static ISpecification<T> ById<T>(Guid id) 
        where T : BaseEntity => new ById<T>(id);
    
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static ISpecification<T> ById<T>(Guid id, Action<ISpecificationBuilder<T>> specification) 
        where T : BaseEntity => new ById<T>(id, specification);
}