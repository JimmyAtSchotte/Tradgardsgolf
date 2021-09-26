using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Infrastructure
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(TradgardsgolfContext db) : base(db)
        {

        }
    }
}
