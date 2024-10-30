namespace Tradgardsgolf.Cqrs.Tests;

public class HandlerFactoryFromBToC : IHandler<CommandCToD>
{
    public CommandCToD Handle()
    {
        return new CommandCToD();
    }
}