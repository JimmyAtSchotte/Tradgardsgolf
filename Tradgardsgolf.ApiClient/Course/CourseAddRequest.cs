namespace Tradgardsgolf.ApiClient.Course
{
    using System;

    public class CourseAddRequest 
    {
        public string Name { get; set; }
        public int Holes { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        private CourseAddRequest() { }

        public static CourseAddRequest Create(Action<CourseAddRequest> properties = null)
        {
            var request = new CourseAddRequest();
            properties?.Invoke(request);

            return request;
        }
    }
}
