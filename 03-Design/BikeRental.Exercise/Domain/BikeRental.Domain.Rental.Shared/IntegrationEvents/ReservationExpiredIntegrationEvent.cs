namespace BikeRental.Domain.Rental.Shared.IntegrationEvents;

public record ReservationExpiredIntegrationEvent(Guid ReservationId, Guid ClientId, DateTimeOffset ExpiredDate);