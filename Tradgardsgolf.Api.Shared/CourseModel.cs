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
        
        public DateTime? ScoreReset { get; set; }
        public string Image { get; set; }
        public int SeasonTableRounds => 6;
        
        public double GetDistance(double longitude, double latitude)
        {
            if (Name == "Testbanan")
                return 0;

            var d1 = this.Latitude * (Math.PI / 180.0);
            var num1 = this.Longitude * (Math.PI / 180.0);
            var d2 = latitude * (Math.PI / 180.0);
            var num2 = longitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
