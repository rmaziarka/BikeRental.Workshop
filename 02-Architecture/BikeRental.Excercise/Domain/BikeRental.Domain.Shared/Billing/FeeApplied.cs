namespace BikeRental.Domain.Shared.Billing;


public record FeeApplied(Guid FeeId, Guid ClientId, decimal Fee, Guid FeeSourceId, DateTimeOffset ApplyDate);