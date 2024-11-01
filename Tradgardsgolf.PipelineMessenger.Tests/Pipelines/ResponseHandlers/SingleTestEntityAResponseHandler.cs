using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Responses;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.ResponseHandlers;

public class SingleTestEntityAResponseHandler : BasePreviousResultHandler<TestEntityAResponse, TestEntityA>
{
    protected override TestEntityAResponse Handle(TestEntityA entity)
    {
        return new TestEntityAResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}