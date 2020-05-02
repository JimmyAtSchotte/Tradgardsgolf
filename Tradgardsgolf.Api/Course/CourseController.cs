using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Api.Course
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<CourseResponse>> Index()
        {
            var courses = _courseService.ListAll().Select(x => new CourseResponse(x));
                          
            return Ok(courses);
        }


        [HttpPost]
        public ActionResult<CourseResponse> Index([FromBody] CourseAddRequest course)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = _courseService.Add(new CourseAddModel(course, userId));

            return Ok(result);
        }
    }

    public class CourseAddModel : ICourseAddModel
    {
        private readonly CourseAddRequest _course;

        public string Name => _course.Name;
        public int Holes => _course.Holes;
        public double Longitude => _course.Longitude;
        public double Latitude => _course.Latitude;
        public int CreatedBy { get; }

        public CourseAddModel(CourseAddRequest course, int userId)
        {
            _course = course;
            CreatedBy = userId;
        }
    }

    public class CourseAddRequest
    {
       public string Name { get; set; }
       public int Holes { get; set; }
       public double Longitude { get; set; }
       public double Latitude { get; set; }
    }

    public class CourseResponse
    {
        private readonly ICourseModelResult _course;

        public int Id => _course.Id;
        public string Name => _course.Name;
        public int Holes => _course.Holes;
        public double Longitude => _course.Longitude;
        public double Latitude => _course.Latitude;
        public CourseCreatedByResponse CreatedBy { get; }
        public DateTime Created => _course.Created;

        public CourseResponse(ICourseModelResult course)
        {
            _course = course;
            CreatedBy = new CourseCreatedByResponse(course.CreatedBy);
        }
    }

    public class CourseCreatedByResponse 
    {
        private readonly ICourseCreatedByModelResult _createdBy;
                
        public int Id => _createdBy.Id;
        public string Name => _createdBy.Name;

        public CourseCreatedByResponse(ICourseCreatedByModelResult createdBy)
        {
            _createdBy = createdBy;
        }
    }
}
