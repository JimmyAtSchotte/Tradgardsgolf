using System;
using Ardalis.Specification;
using Tradgardsgolf.Core.Entities;

namespace Tradgardsgolf.Core.Specifications
{
    public class TournamentsOnCourse : Specification<TournamentCourseDate>
    {
        public TournamentsOnCourse(int courseId, DateTime date)
        {
            Query.Where(x => x.CourseId == courseId && x.Date == date);
            Query.Include(x => x.Tournament);
        }
    }
}