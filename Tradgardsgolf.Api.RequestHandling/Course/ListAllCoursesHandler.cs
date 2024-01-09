﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tradgardsgolf.Contracts.Course;
using Tradgardsgolf.Core.Infrastructure;

namespace Tradgardsgolf.Api.RequestHandling.Course
{
    public class ListAllCoursesHandler(IRepository<Core.Entities.Course> repository) : IRequestHandler<ListAllCoursesCommand, IEnumerable<CourseResponse>>
    {
        public async Task<IEnumerable<CourseResponse>> Handle(ListAllCoursesCommand request, CancellationToken cancellationToken)
        {
            var courses = await repository.ListAsync(cancellationToken);

            return courses.Select(x => new CourseResponse()
            {
                Created = x.Created,
                Holes = x.Holes,
                Id = x.Id,
                Image =x.Image,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Name = x.Name,
                ScoreReset = x.ScoreReset
            });
        }
    }
}
