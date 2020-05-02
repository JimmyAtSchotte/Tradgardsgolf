using Ardalis.GuardClauses;
using System.ComponentModel.DataAnnotations;

namespace Tradgardsgolf.Api.Abstractions.Course
{
    public abstract class BaseCourseAddRequest : ICourseAddRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Holes { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        public void Validate()
        {
            Guard.Against.Null(Name, nameof(Name));
            Guard.Against.NegativeOrZero(Holes, nameof(Holes));
        }
    }
}
