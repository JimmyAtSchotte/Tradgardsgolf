namespace Tradgardsgolf.PipelineMessenger.Messaging;

public interface IMessage
{
    public bool IsOfType<T>();
    public bool IsOfType(Type type);
}
public interface IMessage<TResult> : IMessage;

public static class MessageExtensions
{
    public static bool TryGetMessageReturnType(this IMessage message, out Type returnType)
    {
        returnType = null;
        
        var iMessageInterface = message.GetType()
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessage<>));

        if (iMessageInterface == null)
            return false;
        
        returnType = iMessageInterface.GetGenericArguments()[0];
        
        return true;
    }
}
