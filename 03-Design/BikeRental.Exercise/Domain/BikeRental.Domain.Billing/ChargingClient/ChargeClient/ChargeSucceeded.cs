namespace BikeRental.Domain.Shared.Billing;

public record ChargeSucceeded(Guid FeeId, Guid ClientId, DateTimeOffset ChargeFailedDate);