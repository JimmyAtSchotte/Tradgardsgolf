using System.Windows.Input;
using FluentAssertions;

namespace Tradgardsgolf.Cqrs.Tests;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void ConvertFromAToB()
    {
        var handler = new CqrsFlow([new HandlerStrategy([new HandlerFactoryFromAToBFactory(), new HandlerFactoryFromCToDFactory()])]);
        var result = handler.Handle(new CommandAToB());
        result.Should().BeOfType<ClassB>();
    }
    
    [Test]
    public void ConvertFromCToD()
    {
        var handler = new CqrsFlow([new HandlerStrategy([new HandlerFactoryFromAToBFactory(), new HandlerFactoryFromCToDFactory()])]);
        var result = handler.Handle(new CommandCToD());
        result.Should().BeOfType<ClassD>();
    }
    
    [Test]
    public void ConvertFromAToD()
    {
        var stage1 = new HandlerStrategy([new HandlerFactoryFromAToBFactory()]);
        var stage2 = new HandlerStrategy([new HandlerFactoryFromBToCFactory()]);
        var stage3 = new HandlerStrategy([new HandlerFactoryFromCToDFactory()]);
        
        var handler = new CqrsFlow([stage1, stage2, stage3]);
        var result = handler.Handle(new CommandAToD());
        result.Should().BeOfType<ClassD>();
    }
}

public class CommandCToD : ICommand<ClassD>
{
}

public class ClassD
{
}

public class CommandAToB : ICommand<ClassB>
{
}

public class CommandAToD : ICommand<ClassD>
{
}

public class ClassB
{
}

public interface ICommand<TResult> : ICommand
{
    
}

public interface ICommand
{
    
}

public class HandlerResult
{
    private readonly object _result;

    public HandlerResult(object result)
    {
        _result = result;
    }
    
    public TResult GetValue<TResult>() 
        where TResult : class
    {
        return _result as TResult;
    }

    public Type GetValueType()
    {
        return _result.GetType();
    }
}

public class CqrsFlow
{
    private readonly HandlerStrategy[] _strategies;
    public CqrsFlow(HandlerStrategy[] strategies)
    {
        _strategies = strategies;
    }

    public TResult Handle<TResult>(ICommand<TResult> command) 
        where TResult : class
    {
        var currentResult = new HandlerResult(command);

        foreach (var handlerStrategy in _strategies)
            currentResult = handlerStrategy.Handle(command, currentResult);

        return currentResult.GetValue<TResult>();
    }
}

public interface IHandlerFactory
{
    bool AppliesTo(ICommand command, HandlerResult handlerResult);
    IHandler Create(ICommand command, HandlerResult handlerResult);
}

public interface IHandler
{
    object Handle();
}

public class HandlerFactoryFromAToBFactory : IHandlerFactory
{
    public bool AppliesTo(ICommand command, HandlerResult handlerResult)
    {
        return command.GetType() == typeof(CommandAToB) || command.GetType() == typeof(CommandAToD);
    }

    public IHandler Create(ICommand command, HandlerResult handlerResult)
    {
        return new HandlerFactoryFromAToB();
    }
}

public class HandlerFactoryFromAToB : IHandler
{
    public object Handle()
    {
        return new ClassB();
    }
}


public class HandlerFactoryFromCToDFactory : IHandlerFactory
{
    public bool AppliesTo(ICommand command, HandlerResult handlerResult)
    {
        return command.GetType() == typeof(CommandCToD) || handlerResult.GetValueType() == typeof(CommandCToD);
    }

    public IHandler Create(ICommand command, HandlerResult handlerResult)
    {
        return new HandlerFactoryFromCToD();
    }
}

public class HandlerFactoryFromCToD : IHandler
{
    public object Handle()
    {
        return new ClassD();
    }
}


public class HandlerFactoryFromBToCFactory : IHandlerFactory
{
    public bool AppliesTo(ICommand command, HandlerResult handlerResult)
    {
        return handlerResult.GetValueType() == typeof(ClassB);
    }

    public IHandler Create(ICommand command, HandlerResult handlerResult)
    {
        return new HandlerFactoryFromBToC();
    }
}

public class HandlerFactoryFromBToC : IHandler
{
    public object Handle()
    {
        return new CommandCToD();
    }
}