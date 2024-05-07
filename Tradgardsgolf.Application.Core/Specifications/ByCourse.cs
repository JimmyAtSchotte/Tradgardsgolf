using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class ByCourse : Specification<Scorecard>
    {
        public ByCourse(Guid courseId)
        {
            Query.Where(x => x.CourseId == courseId);
        }
    }
}