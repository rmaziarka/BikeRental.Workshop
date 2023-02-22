namespace BikeRental.Domain.Shared.Billing;

public record FeeCalculationFinished(Guid FeeId, Guid ClientId, DateTimeOffset FinishDate);