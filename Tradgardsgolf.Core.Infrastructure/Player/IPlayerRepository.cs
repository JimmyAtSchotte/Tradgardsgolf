using System.Collections.Generic;

namespace Tradgardsgolf.Core.Infrastructure.Player
{
    public interface IPlayerRepository 
    {
        bool CheackIfMailExists(string email);
        IPlayerDtoResult GetByEmail(string email);
        IEnumerable<IPlayerDtoResult> GetPlayersThatHasPlayedOnCourse(int courseId);
        IPlayerDtoResult Find(IFindPlayerPlayedOnCourseDto dto);
        IPlayerDtoResult Add(IAddPlayerDto dto);
    }

    public interface IAddPlayerDto
    {
        string Name { get; }
    }

    public interface IFindPlayerPlayedOnCourseDto
    {
        int CourseId { get; }
        string Name { get; }
    }
}
