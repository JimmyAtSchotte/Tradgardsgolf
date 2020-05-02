using System;

namespace Tradgardsgolf.Api.Abstractions.Course
{
    public class CourseResponse
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public int Holes { get; set; }
       public double Longitude { get; set; }
       public double Latitude { get; set; }
       public CourseCreatedByResponse CreatedBy { get; set; }
       public DateTime Created { get; }

    }
}
