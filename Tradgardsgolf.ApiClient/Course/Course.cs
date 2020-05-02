using System;

namespace Tradgardsgolf.ApiClient.Course
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Holes { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public CourseCreatedBy CreatedBy { get; set; }
        public DateTime Created { get; set; }
    }
}
