namespace Tradgardsgolf.Cqrs;

public interface IHandler<out TResult>
{
    TResult Handle();
}