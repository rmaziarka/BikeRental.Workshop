namespace BikeRental.Domain.Shared.Billing;

public record FeeCancelled(Guid FeeId, Guid ClientId, string CancellationDescription,
    DateTimeOffset CancellationDate);