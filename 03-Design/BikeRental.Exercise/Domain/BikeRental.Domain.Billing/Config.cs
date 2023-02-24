using System.Reflection;
using BikeRental.Domain.Billing.FeeRates;
using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
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


public static class IntegrationEventBusExtensions
{
    public static IServiceCollection RegisterAllBillingIntegrationEventHandlers(this IServiceCollection services)
    {
        services.RegisterAllIntegrationEventHandlers(Assembly.GetExecutingAssembly());
        
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

    public static IApplicationBuilder SetupBillingRepositoriesData(this IApplicationBuilder builder)
    {
        var eventRateRepo = builder.ApplicationServices.GetService<Repository<EventRate>>();

        var reservationMadeRate = new EventRate()
        {
            Id = Guid.NewGuid(),
            EventName = nameof(ReservationMadeIntegrationEvent),
            FeeRates = new[] { new FeeRate() { Currency = "PLN", Amount = 10, Description = "reservation made" } }
        };
        eventRateRepo.Create(reservationMadeRate);
        
        var bikeRentedRate = new EventRate()
        {
            Id = Guid.NewGuid(),
            EventName = nameof(BikeRentedIntegrationEvent),
            FeeRates = new[] { new FeeRate() { Currency = "PLN", Amount = 20, Description = "bike rented" } }
        };
        eventRateRepo.Create(bikeRentedRate);
        
        var bikeReturnedRate = new EventRate()
        {
            Id = Guid.NewGuid(),
            EventName = nameof(RentalFinishedIntegrationEvent),
            FeeRates = new[] { new FeeRate() { Currency = "PLN", Amount = 15, Description = "rental finished" } }
        };
        eventRateRepo.Create(bikeReturnedRate);
        
        var bikeReturnedOutsideStation = new EventRate()
        {
            Id = Guid.NewGuid(),
            EventName = nameof(RentalFinishedOutsideStationIntegrationEvent),
            FeeRates = new[]
            {
                new FeeRate() { Currency = "PLN", Amount = 15, Description = "rental finished" },
                new FeeRate() { Currency = "PLN", Amount = 5, Description = "return outside station" }
                
            }
        };
        eventRateRepo.Create(bikeReturnedOutsideStation);
        

        return builder;
    }
}