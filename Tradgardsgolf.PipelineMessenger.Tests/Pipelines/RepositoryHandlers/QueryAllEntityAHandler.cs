using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;
using Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Messages;

namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.RepositoryHandlers;

public class QueryAllEntityAHandler : BaseMessageHandler<TestEntityA[], QueryAllTestEntityA>
{
    protected override TestEntityA[] Handle(QueryAllTestEntityA message)
    {
        return new[]
        {
            new TestEntityA(),
            new TestEntityA(),
            new TestEntityA(),
        };
    }
}