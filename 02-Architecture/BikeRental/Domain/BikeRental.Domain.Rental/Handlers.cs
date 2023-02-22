using System.Reflection;
using BikeRental.Tech;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRental.Domain.Rental;

public static class CommandBusExtensions
{
    public static IServiceCollection RegisterAllRentalCommandHandlers(this IServiceCollection services)
    {
        services.RegisterAllCommandHandlers(Assembly.GetExecutingAssembly());
        
        return services;
    }
}

public static class EventBusExtensions
{
    public static IServiceCollection RegisterAllRentalEventHandlers(this IServiceCollection services)
    {
        services.RegisterAllEventHandlers(Assembly.GetExecutingAssembly());
        
        return services;
    }
}