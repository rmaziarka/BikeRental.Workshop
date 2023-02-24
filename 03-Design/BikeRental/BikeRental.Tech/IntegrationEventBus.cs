using System.Collections;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.DependencyInjection;
namespace BikeRental.Tech;

// simple example
// do not use in production
public class IntegrationEventBus
{
    private readonly IServiceProvider _serviceProvider;
    public IntegrationEventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Publish(object @event)
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {@event.GetType().Name} published.");
        Console.WriteLine("--------------------");
        Type eventType = @event.GetType();
        Type openHandlerType = typeof(IIntegrationEventHandler<>);
        Type handlerType = openHandlerType.MakeGenericType(eventType);
        IEnumerable<object> handlers = _serviceProvider.GetServices(handlerType);
        
        foreach (object handler in handlers)
        {
            var action = () =>
            {
                // added so that command handling will finish earlier
                Thread.Sleep(50);
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Using {handler.GetType().Name} to handle {@event.GetType().Name}.");

                var method = typeof(IIntegrationEventHandler<>)
                    .MakeGenericType(eventType)
                    .GetMethod(nameof(IIntegrationEventHandler<object>.Handle));
                
                method.Invoke(handler, new object[] { @event });
            };
            Task.Run(action);
        }
    }
}



public interface IIntegrationEventHandler<T>
{
    void Handle(T @event);
}


public static class IntegrationEventBusExtensions
{
    public static IServiceCollection RegisterAllIntegrationEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        var type = typeof(IIntegrationEventHandler<>);
        var eventHandlers = assembly
            .GetTypes()
            .Where(t => t.IsClass)
            .Where(t => t
                .GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)
            )
            .ToList();

        foreach (Type eventHandler in eventHandlers)
        {
            var interfaces = eventHandler
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == type);
            
            foreach (var @interface in interfaces)
            {
                services.AddTransient(@interface, eventHandler);
            }
        }

        return services;
    }
}