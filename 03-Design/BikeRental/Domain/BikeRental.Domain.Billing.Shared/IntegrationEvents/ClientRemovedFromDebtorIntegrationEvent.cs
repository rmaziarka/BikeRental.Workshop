namespace BikeRental.Domain.Billing.Shared.IntegrationEvents;

public record ClientRemovedFromDebtorIntegrationEvent (Guid ClientId);