using System.Reflection;
using BikeRental.Tech;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRental.Domain.Billing;

public static class CommandBusExtensions
{
    public static IServiceCollection RegisterAllBillingCommandHandlers(this IServiceCollection services)
    {
        services.RegisterAllCommandHandlers(Assembly.GetExecutingAssembly());
        
        return services;
    }
}

public static class EventBusExtensions
{
    public static IServiceCollection RegisterAllBillingEventHandlers(this IServiceCollection services)
    {
        services.RegisterAllEventHandlers(Assembly.GetExecutingAssembly());
        
        return services;
    }
}

public static class RepositoryExtensions
{
    public static IServiceCollection RegisterBillingRepositories(this IServiceCollection services)
    {
        services.RegisterAllRepositories(Assembly.GetExecutingAssembly());
        
        return services;
    }
}