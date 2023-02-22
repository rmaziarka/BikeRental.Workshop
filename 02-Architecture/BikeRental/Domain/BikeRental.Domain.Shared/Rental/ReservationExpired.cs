namespace BikeRental.Domain.Shared.Rental;

public record ReservationExpired(Guid ReservationId, Guid ClientId);