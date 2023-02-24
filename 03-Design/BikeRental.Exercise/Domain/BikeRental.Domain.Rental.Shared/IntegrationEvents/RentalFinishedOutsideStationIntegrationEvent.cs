namespace BikeRental.Domain.Rental.Shared.IntegrationEvents;

public record RentalFinishedOutsideStationIntegrationEvent(Guid RentalId, Guid ClientId, DateTimeOffset StartDate, DateTimeOffset FinishDate);