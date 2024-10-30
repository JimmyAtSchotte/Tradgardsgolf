namespace Tradgardsgolf.Cqrs.Tests;

public class HandlerFactoryFromAToB : IHandler<ClassB>
{
    public ClassB Handle()
    {
        return new ClassB();
    }
}