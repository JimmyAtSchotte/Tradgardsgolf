using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.RepositoryHandlers;

public class QueryAllEntityAHandler : BaseMessageHandler<TestEntityA[], QueryAllTestEntityA>
{
    protected override Task<TestEntityA[]> HandleAsync(QueryAllTestEntityA message)
    {
        return Task.FromResult(new[]
        {
            new TestEntityA(),
            new TestEntityA(),
            new TestEntityA(),
        });
    }
}