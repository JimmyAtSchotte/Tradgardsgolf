namespace Tradgardsgolf.Cqrs;


public abstract class BaseCommand<TResult> : ICommand<TResult>
{
    public bool IsOfType<T>() => GetType() == typeof(T);
}

public interface ICommand
{
    public bool IsOfType<T>();
}
public interface ICommand<TResult> : ICommand;
