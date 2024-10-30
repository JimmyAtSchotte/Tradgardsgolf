namespace Tradgardsgolf.Cqrs;

public class CqrsPipeline
{
    private readonly IPipeline[] _pipelines;
    public CqrsPipeline(IPipeline[] pipelines)
    {
        _pipelines = pipelines;
    }

    public TResult? Handle<TResult>(ICommand<TResult> command) 
        where TResult : class
    {
        var currentResult = new HandlerResult(null);
        
        foreach (var pipeline in _pipelines)
        {
            currentResult = pipeline.Handle(command, currentResult);
        }

        return currentResult.GetValue<TResult>();
    }
}