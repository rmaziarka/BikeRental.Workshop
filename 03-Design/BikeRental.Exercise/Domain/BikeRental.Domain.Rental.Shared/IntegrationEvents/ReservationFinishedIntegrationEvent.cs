namespace BikeRental.Domain.Rental.Shared.IntegrationEvents;

public record ReservationFinishedIntegrationEvent(Guid ReservationId, Guid ClientId, DateTimeOffset FinishDate);