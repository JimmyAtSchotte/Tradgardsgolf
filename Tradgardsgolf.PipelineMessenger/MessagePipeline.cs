namespace Tradgardsgolf.PipelineMessenger;

public class MessagePipeline(IPipeline[] pipelines)
{
    public TResult? Handle<TResult>(IMessage<TResult> message) 
        where TResult : class
    {
        var currentResult = HandlerResult.Empty();
        
        foreach (var pipeline in pipelines)
            currentResult = pipeline.Handle(message, currentResult);

        return currentResult.TryGetValue<TResult>(out var result) 
            ? result 
            : default;
    }
}