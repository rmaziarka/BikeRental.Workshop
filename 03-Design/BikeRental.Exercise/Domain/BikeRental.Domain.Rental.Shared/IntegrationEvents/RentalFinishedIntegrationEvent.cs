namespace BikeRental.Domain.Rental.Shared.IntegrationEvents;

public record RentalFinishedIntegrationEvent(Guid RentalId, Guid ClientId, DateTimeOffset StartDate, DateTimeOffset FinishDate);