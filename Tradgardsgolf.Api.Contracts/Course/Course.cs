using System;
using Tradgardsgolf.Contracts.Players;

namespace Tradgardsgolf.Contracts.Course
{
    public record Course
    {             
        public int Id { get; init; }
        public string Name { get; init; }
        public int Holes { get; init; }
        public double Longitude { get; init;  }
        public double Latitude { get; init; }
        public DateTime Created { get; init; }
        
        public DateTime? ScoreReset { get; init; }
        public string Image { get; init; }
        public int SeasonTableRounds => 6;
    }
}
