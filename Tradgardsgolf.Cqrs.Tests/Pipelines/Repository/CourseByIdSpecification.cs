using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.Cqrs.Tests.Pipelines.Messages;

namespace Tradgardsgolf.Cqrs.Tests.Pipelines.Repository;

public class CourseById : BaseMessageHandler<Course, ICourseByIdMessage>
{
    protected override Course Handle(ICourseByIdMessage message)
    {
        return Course.Create(Guid.NewGuid(), p => p.Id = message.CourseId);
    }
}

