using System.Reflection;
using BikeRental.Domain.Rental.Rental.RentBike;
using BikeRental.Domain.Rental.Rental.CancelReservation;
using BikeRental.Domain.Rental.Rental.RentBikeBasedOnReservation;
using BikeRental.Domain.Rental.Reservation.MakeReservation;
using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRental.Domain.Rental;

public static class Endpoints
{
    public static IEndpointRouteBuilder UseRentalEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints
            .UseMakeReservationEndpoint()
            .UseCancelReservationEndpoint()
            .UseRentBikeEndpoint()
            .UseRentBikeBasedOnReservationEndpoint()
    ;
}

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

public static class IntegrationEventBusExtensions
{
    public static IServiceCollection RegisterAllRentalIntegrationEventHandlers(this IServiceCollection services)
    {
        services.RegisterAllIntegrationEventHandlers(Assembly.GetExecutingAssembly());

        return services;
    }
}

public static class RepositoryExtensions
{
    public static IServiceCollection RegisterRentalRepositories(this IServiceCollection services)
    {
        services.RegisterAllRepositories(Assembly.GetExecutingAssembly());
        
        return services;
    }

    public static IApplicationBuilder SetupRentalRepositoriesData(this IApplicationBuilder builder)
    {
        var clientContext = builder.ApplicationServices.GetService<ClientContext>();
        var clientRepo = builder.ApplicationServices.GetService<Repository<Client.Client>>();
        
        clientRepo.Create(new Client.Client(){Id = clientContext.Id, IsActive = true});

        return builder;
    }
}