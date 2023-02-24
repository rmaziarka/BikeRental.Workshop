namespace BikeRental.Domain.Rental.Shared.IntegrationEvents;

public record ReservationMadeIntegrationEvent(Guid ReservationId, Guid BikeId, Guid ClientId, DateTimeOffset StartDate, DateTimeOffset ExpirationDate);