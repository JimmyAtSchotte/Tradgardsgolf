namespace Tradgardsgolf.Cqrs;

public class MessagePipeline
{
    private readonly IPipeline[] _pipelines;
    public MessagePipeline(IPipeline[] pipelines)
    {
        _pipelines = pipelines;
    }

    public TResult? Handle<TResult>(IMessage<TResult> message) 
        where TResult : class
    {
        var currentResult = HandlerResult.Empty();
        
        foreach (var pipeline in _pipelines)
        {
            currentResult = pipeline.Handle(message, currentResult);
        }

        return currentResult.GetValue<TResult>();
    }
}