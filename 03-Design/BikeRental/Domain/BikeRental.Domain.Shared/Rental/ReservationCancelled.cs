namespace BikeRental.Domain.Shared.Rental;

public record ReservationCancelled(Guid ReservationId, Guid ClientId, DateTimeOffset CancellationDate);