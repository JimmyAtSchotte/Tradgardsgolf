using Tradgardsgolf.Core.Entities;
using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Repository;

public class CourseByIdHandler : BaseMessageHandler<Course, ICourseByIdMessage>
{
    protected override Course Handle(ICourseByIdMessage message)
    {
        return Course.Create(Guid.NewGuid(), p => p.Id = message.CourseId);
    }
}