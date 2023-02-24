using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
namespace BikeRental.Tech;

// simple example
// do not use in production
public class CommandBus
{
    private readonly IServiceProvider _serviceProvider;
    public CommandBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Handle<TCommand>(TCommand command)
    {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Command {typeof(TCommand).Name} sent to handle.");
        Type commandType = command.GetType();
        Type openHandlerType = typeof(ICommandHandler<>);
        Type handlerType = openHandlerType.MakeGenericType(commandType);
        IEnumerable<object> handlers = _serviceProvider.GetServices(handlerType);
        foreach (object handler in handlers)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Using {handler.GetType().Name} to handle command.");
            
            handlerType
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(ICommandHandler<TCommand>.Handle))
                .Invoke(handler, new object[] { command });
        }
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: Command {typeof(TCommand).Name} handled.");
        Console.WriteLine("--------------------");
    }
}

public interface ICommandHandler<T>
{
    void Handle(T command);
}


public static class CommandBusExtensions
{
    public static IServiceCollection RegisterAllCommandHandlers(this IServiceCollection services, Assembly assembly)
    {
        var type = typeof(ICommandHandler<>);
        var commandHandlers = assembly
            .GetTypes()
            .Where(t => t.IsClass)
            .Where(t => t
                .GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type)
            )
            .ToList();

        foreach (Type commandHandler in commandHandlers)
        {
            var interfaces = commandHandler
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == type);
            
            foreach (var @interface in interfaces)
            {
                services.AddTransient(@interface, commandHandler);
            }
        }

        return services;
    }
}