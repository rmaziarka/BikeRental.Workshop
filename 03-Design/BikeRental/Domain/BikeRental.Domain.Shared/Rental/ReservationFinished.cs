namespace BikeRental.Domain.Shared.Rental;

public record ReservationFinished(Guid ReservationId, Guid ClientId, DateTimeOffset FinishedDate);