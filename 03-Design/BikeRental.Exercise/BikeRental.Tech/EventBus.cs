using System.Collections;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
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
        if (!entity.Events.Any())
            return;
        
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Publishing events from {entity.GetType().Name} entity.");
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

    public void PublishEventsFromEntities()
    {
        var aggregates = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(ass => ass.FullName.Contains("BikeRental"))
            .SelectMany(ass => ass.GetTypes())
            .Where(t => t.GetInterfaces().Any(i => i == typeof(IAggregateRoot)))
            .ToList();

        foreach (var aggregate in aggregates)
        {
            var repositoryType = typeof(Repository<>)
                .MakeGenericType(aggregate);

            var repo = _serviceProvider.GetService(repositoryType);

            var entities = repositoryType
                .GetField("_list", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(repo) as IEnumerable;

            foreach (var entity in entities)
            {
                var idType = 
                    aggregate.BaseType.GenericTypeArguments.First();

                var method = typeof(EventBus)
                    .GetMethod(nameof(EventBus.PublishFromEntity))
                    .MakeGenericMethod(idType);
                
                method.Invoke(this, new object[] { entity });
            }
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