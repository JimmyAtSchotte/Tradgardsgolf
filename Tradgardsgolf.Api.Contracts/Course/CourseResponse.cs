using System;
using Tradgardsgolf.Contracts.Players;
using Tradgardsgolf.Contracts.Types;

namespace Tradgardsgolf.Contracts.Course
{
    public record CourseResponse
    {             
        public int Id { get; init; }
        public string Name { get; init; }
        public int Holes { get; init; }
        public double Longitude { get; init;  }
        public double Latitude { get; init; }
        public DateTime Created { get; init; }
        public DateTime? ScoreReset { get; init; }
        public ImageReference ImageReference { get; init; }
        public int SeasonTableRounds => 6;

        public string OwnerEmail { get; init; }
        
    }
}
