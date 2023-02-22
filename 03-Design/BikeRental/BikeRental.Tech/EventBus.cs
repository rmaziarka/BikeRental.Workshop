using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace BikeRental.Tech;

// simple example
// do not use in production
public class EventBus
{
    private readonly IServiceProvider _serviceProvider;
    public EventBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void PublishFromEntity<T>(Entity<T> entity)
    {
        foreach (var @event in entity.Events)
        {
            Publish(@event);
        }

        entity.ClearEvents();
    }
    
    public void Publish(object @event)
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Event {@event.GetType().Name} published.");
        Type eventType = @event.GetType();
        Type openHandlerType = typeof(IEventHandler<>);
        Type handlerType = openHandlerType.MakeGenericType(eventType);
        IEnumerable<object> handlers = _serviceProvider.GetServices(handlerType);
        
        foreach (object handler in handlers)
        {
            var action = () =>
            {
                // added so that command handling will finish earlier
                Thread.Sleep(50);
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Using {handler.GetType().Name} to handle {@event.GetType().Name} event.");

                var method = typeof(IEventHandler<>)
                    .MakeGenericType(eventType)
                    .GetMethod(nameof(IEventHandler<object>.Handle));
                
                method.Invoke(handler, new object[] { @event });
            };
            Task.Run(action);
        }
    }
}



public interface IEventHandler<T>
{
    void Handle(T @event);
}


public static class EventBusExtensions
{
    public static IServiceCollection RegisterAllEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        var type = typeof(IEventHandler<>);
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