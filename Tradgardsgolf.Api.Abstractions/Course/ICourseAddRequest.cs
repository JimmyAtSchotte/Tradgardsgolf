using System.Collections.Generic;
using System.Text;

namespace Tradgardsgolf.Api.Abstractions.Course
{
    public interface ICourseAddRequest : IRequestValidation
    {
        string Name { get; }
        int Holes { get; }
        double Longitude { get; }
        double Latitude { get; }
    }
}
