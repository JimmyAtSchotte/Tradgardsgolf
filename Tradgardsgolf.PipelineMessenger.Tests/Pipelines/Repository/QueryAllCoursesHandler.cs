using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Repository;

public class QueryAllCoursesHandler : BaseMessageHandler<Course[], QueryAllCourses>
{
    protected override Course[] Handle(QueryAllCourses message)
    {
        return new[]
        {
            Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid()),
            Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid()),
            Course.Create(Guid.NewGuid(), p => p.Id = Guid.NewGuid()),
        };
    }
}