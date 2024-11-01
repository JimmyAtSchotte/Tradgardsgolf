using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Responses;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.ResponseHandlers;

public class MultiTestEntityAResponseHandler : BasePreviousResultHandler<TestEntityAResponse[], TestEntityA[]>
{
 
    protected override Task<TestEntityAResponse[]> HandleAsync(TestEntityA[] entities)
    {
        return Task.FromResult(entities.Select(entity => new TestEntityAResponse()
        {
            Id = entity.Id,
            Name = entity.Name,
        }).ToArray());
    }
}