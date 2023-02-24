namespace BikeRental.Domain.Rental.Shared.IntegrationEvents;

public record BikeRentedIntegrationEvent(Guid RentalId, Guid BikeId, Guid ClientId, DateTimeOffset StartDate);