using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tradgardsgolf.Core.Services.Course;

namespace Tradgardsgolf.Blazor.Data
{
    public class Course 
    {             
        public int Id { get; set; }
        public string Name { get; set; }
        public int Holes { get; set; }
        public double Longitude { get; set;  }
        public double Latitude { get; set; }
        public CourseCreatedBy CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string Image { get; set; }

        public static Course Create(ICourseModelResult course)
        {
            return new Course()
            {
                Id = course.Id,
                Name = course.Name,
                Holes = course.Holes,
                Longitude = course.Longitude,
                Latitude = course.Latitude,
                Created = course.Created,
                Image = course.Image,
                CreatedBy = CourseCreatedBy.Create(course.CreatedBy)
            };         
        }

        public class CourseCreatedBy
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public static CourseCreatedBy Create(ICourseCreatedByModelResult createdBy)
            {
                return new CourseCreatedBy()
                {
                    Id = createdBy.Id,
                    Name = createdBy.Name
                };
            }
        }
    }
}
