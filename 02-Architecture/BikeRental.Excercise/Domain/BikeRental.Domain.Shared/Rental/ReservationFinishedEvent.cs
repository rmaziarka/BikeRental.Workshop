namespace BikeRental.Domain.Shared.Rental;

public record ReservationFinishedEvent(Guid ReservationId, DateTimeOffset FinishedDate);