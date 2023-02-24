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

    public void Publish<TEvent>(TEvent @event)
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Event {typeof(TEvent).Name} published.");
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
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Using {handler.GetType().Name} to handle {typeof(TEvent).Name} event.");
                
                handlerType
                    .GetTypeInfo()
                    .GetDeclaredMethod(nameof(IEventHandler<TEvent>.Handle))
                    .Invoke(handler, new object[] { @event });
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