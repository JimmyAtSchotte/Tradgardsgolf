using System;

namespace Tradgardsgolf.Blazor.Wasm.Data
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

        public class CourseCreatedBy
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
