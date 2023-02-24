namespace BikeRental.Domain.Billing.Shared.IntegrationEvents;

public record ClientMarkedAsDebtorIntegrationEvent (Guid ClientId);