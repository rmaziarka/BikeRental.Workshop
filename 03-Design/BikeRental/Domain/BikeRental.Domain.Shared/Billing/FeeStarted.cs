namespace BikeRental.Domain.Shared.Billing;

public record FeeStarted(Guid FeeId, Guid ClientId, Guid SourceId, FeeSourceType feeSourceType,
    DateTimeOffset StartDate);

