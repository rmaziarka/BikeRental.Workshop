namespace BikeRental.Domain.Shared.Billing;

public record ChargeFailed(Guid FeeId, Guid ClientId, DateTimeOffset ChargeFailedDate);