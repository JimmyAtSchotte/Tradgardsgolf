using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Api.Endpoints.Courses
{
    public class List : BaseEndpoint<CourseListRequest, IEnumerable<CourseListResponse>>
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
        public override ActionResult<IEnumerable<CourseListResponse>> Handle([FromQuery] CourseListRequest request)
        {
            var courses = _courseService.ListAll();

            return Ok(courses.Select(x => new CourseListResponse(x)));
        }
    }

    public class CourseListResponse
    {
        private readonly ICourseModelResult _courseModelResult;

        public int Id => _courseModelResult.Id;
        public string Name => _courseModelResult.Name;
        public int Holes => _courseModelResult.Holes;
        public double Longitude => _courseModelResult.Longitude;
        public double Latitude => _courseModelResult.Latitude;
        public string Image => _courseModelResult.Image;
        public DateTime Created => _courseModelResult.Created;
        
        public CourseListResponse(ICourseModelResult courseModelResult)
        {
            _courseModelResult = courseModelResult;
        }
    }

    public class CourseListRequest
    {
    }
}