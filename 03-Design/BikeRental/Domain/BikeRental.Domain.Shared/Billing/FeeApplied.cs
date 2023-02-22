namespace BikeRental.Domain.Shared.Billing;


public record FeeApplied(Guid FeeId, Guid ClientId, string Currency, decimal Price, string Description,
    DateTimeOffset ApplyDate);