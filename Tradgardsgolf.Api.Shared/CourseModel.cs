using System;

namespace Tradgardsgolf.Api.Shared
{
    public class CourseModel 
    {             
        public int Id { get; set; }
        public string Name { get; set; }
        public int Holes { get; set; }
        public double Longitude { get; set;  }
        public double Latitude { get; set; }
        public PlayerModel CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string Image { get; set; }
        public int SeasonTableRounds => 6;
    }
}
