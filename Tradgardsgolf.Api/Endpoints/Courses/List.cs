using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using Tradgardsgolf.Api.Shared;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Api.Endpoints.Courses
{
    public class List : BaseEndpoint<IEnumerable<CourseModel>>
    {
        private readonly ICourseService _courseService;

        public List(ICourseService courseService)
        {
            _courseService = courseService;
        }
        
        [HttpGet("Courses")]
        [SwaggerOperation(
            OperationId = "Course.List",
            Tags = new[] { "Courses" })
        ]
        public override ActionResult<IEnumerable<CourseModel>> Handle()
        {
            var courses = _courseService.ListAll();

            var models = courses.Select(x => new CourseModel()
            {
                Created = x.Created,
                Holes = x.Holes,
                Id = x.Id,
                Image = x.Image,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Name = x.Name,
                CreatedBy = new PlayerModel()
                {
                    Id = x.CreatedBy.Id,
                    Name = x.Name
                }
            });

            return Ok(models);
        }
    }
}