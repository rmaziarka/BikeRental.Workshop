namespace BikeRental.Domain.Rental.Shared.IntegrationEvents;

public record ReservationCancelledIntegrationEvent(Guid ReservationId, Guid ClientId, DateTimeOffset CancellationDate);