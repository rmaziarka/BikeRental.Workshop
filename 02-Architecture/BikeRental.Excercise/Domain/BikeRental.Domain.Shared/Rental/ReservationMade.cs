namespace BikeRental.Domain.Shared.Rental;

public record ReservationMade(Guid ReservationId, Guid BikeId, Guid ClientId, DateTimeOffset StartDate, DateTimeOffset ExpirationDate);