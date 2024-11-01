namespace Tradgardsgolf.PipelineMessenger.Tests.Pipelines.Entities;

public interface IEntity
{
    Guid Id { get; set; }
}


public class TestEntityA : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public TestEntityA()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
    }

    public TestEntityA(Guid id)
    {
        Id = id;
        Name = string.Empty;
    }
}