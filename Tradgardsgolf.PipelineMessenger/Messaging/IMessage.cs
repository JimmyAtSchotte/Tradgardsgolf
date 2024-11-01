namespace Tradgardsgolf.PipelineMessenger.Messaging;

public interface IMessage
{
    public bool IsOfType<T>();
    public bool IsOfType(Type type);
}
public interface IMessage<TResult> : IMessage;

public static class MessageExtensions
{
    public static bool IsResultArray(this IMessage message)
    {
        var iMessageInterface = message.GetType()
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMessage<>));

        if (iMessageInterface == null)
            return false;
        
        var returnType = iMessageInterface.GetGenericArguments()[0];
        
        return returnType.IsArray;
    }
}
