namespace Tradgardsgolf.PipelineMessenger.Messaging;

public interface IMessage
{
    public bool IsOfType<T>();
    public bool IsOfType(Type type);
}
public interface IMessage<TResult> : IMessage;
