namespace Tradgardsgolf.Cqrs.Tests;

public class HandlerFactoryFromCToD : IHandler<ClassD>
{
    public ClassD Handle()
    {
        return new ClassD();
    }
}