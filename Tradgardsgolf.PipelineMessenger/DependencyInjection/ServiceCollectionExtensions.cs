using Microsoft.Extensions.DependencyInjection;
using Tradgardsgolf.PipelineMessenger.Handlers;
using Tradgardsgolf.PipelineMessenger.Pipelines;

namespace Tradgardsgolf.PipelineMessenger.DependencyInjection;

public class MessagePipelineBuilder
{
    private readonly IServiceCollection _services;
    private readonly List<Type[]> _pipelines;

    public MessagePipelineBuilder(IServiceCollection  services)
    {
        _services = services;
        _pipelines = new List<Type[]>();
    }
    
    public MessagePipelineBuilder AddPipeline(params Type[] handlers)
    {
        foreach (var handler in handlers)
        {
            if (_services.All(service => service.ServiceType != handler))
                _services.AddTransient(handler);
        }
        
        _pipelines.Add(handlers);
        
        return this;
    }

    public MessagePipeline Build(IServiceProvider provider)
    {
        var pipelines = _pipelines.Select(types => new Pipeline(types
                                                                    .Where(type => typeof(IHandler).IsAssignableFrom(type))
                                                                    .Select(provider.GetRequiredService)
                                                                    .Cast<IHandler>()
                                                                    .ToArray()
                                                                ))
                                  .Cast<IPipeline>()
                                  .ToArray();
        
        return new MessagePipeline(pipelines);
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagePipeline(this IServiceCollection services, Action<MessagePipelineBuilder> configure)
    {
        var builder = new MessagePipelineBuilder(services);
        configure(builder);
        
        services.AddSingleton(provider => builder.Build(provider));
        return services;
    }
}