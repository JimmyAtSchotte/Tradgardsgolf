namespace Tradgardsgolf.PipelineMessenger;

public interface IMessage
{
    public bool IsOfType<T>();
}
public interface IMessage<TResult> : IMessage;
