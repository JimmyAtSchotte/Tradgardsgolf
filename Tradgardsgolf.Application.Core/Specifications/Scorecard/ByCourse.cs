﻿using System;
using System.Diagnostics.CodeAnalysis;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Scorecard;


[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public static partial class ScorecardSpecificationExtensions
{
    public static ISpecification<Entities.Scorecard> ByCourse(this SpecificationSet<Entities.Scorecard> set,
        Guid courseId)
        => new ByCourse(courseId);
}

internal sealed class ByCourse : SpecificationEquatable<Entities.Scorecard, ByCourse>
{
    public ByCourse(Guid courseId) : base(courseId)
    {
        Query.Where(x => x.CourseId == courseId);
    }
}