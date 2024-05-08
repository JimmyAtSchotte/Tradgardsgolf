using System;
using Ardalis.Specification;

namespace Tradgardsgolf.Core.Specifications.Course;

public class ById : Specification<Entities.Course>
{
    public ById(Guid courseId)
    {
        Query.Where(x => x.Id == courseId);
    }
}