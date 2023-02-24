using BikeRental.Domain.Billing.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsActive;

public class MarkClientAsActiveEventHandlers : IEventHandler<ClientRemovedFromDebtorIntegrationEvent>
{
    private readonly CommandBus _commandBus;

    public MarkClientAsActiveEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ClientRemovedFromDebtorIntegrationEvent @event)
    {
        var command = new MarkClientAsActive(@event.ClientId);
        _commandBus.Handle(command);
    }
}