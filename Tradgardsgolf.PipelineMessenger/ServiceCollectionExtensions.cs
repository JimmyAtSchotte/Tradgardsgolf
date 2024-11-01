using Microsoft.Extensions.DependencyInjection;

namespace Tradgardsgolf.PipelineMessenger;

public class MessagePipelineOptions
{
    private readonly IServiceProvider _services;
    private readonly List<IPipeline> _pipelines;

    public MessagePipelineOptions(IServiceProvider services)
    {
        _services = services;
        _pipelines = new List<IPipeline>();
    }

    public IPipeline[] GetPipelines() => _pipelines.ToArray();

    public MessagePipelineOptions AddPipeline(IPipeline pipeline)
    {
        _pipelines.Add(pipeline);
        return this;
    }
    
    public MessagePipelineOptions AddPipeline(params Type[] handlers)
    {
        _pipelines.Add(new Pipeline(handlers
            .Where(type => typeof(IHandler).IsAssignableFrom(type))
            .Select(type => _services.GetRequiredService(type))
            .Cast<IHandler>()
            .ToArray()));
        
        return this;
    }
}

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddMessagePipeline(this ServiceCollection serviceCollection, Action<MessagePipelineOptions> configureOptions)
    {
        serviceCollection.AddScoped<MessagePipeline>(services =>
        {
            var messagePipelineOptions = new MessagePipelineOptions(services);
            configureOptions(messagePipelineOptions);
            
            return new MessagePipeline(messagePipelineOptions.GetPipelines());
        });

        return serviceCollection;
    }
}