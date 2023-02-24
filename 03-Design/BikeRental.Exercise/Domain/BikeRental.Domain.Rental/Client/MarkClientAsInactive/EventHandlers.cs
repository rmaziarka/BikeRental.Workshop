using BikeRental.Domain.Billing.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsInactive;

public class MarkClientAsInactiveEventHandlers : IEventHandler<ClientMarkedAsDebtorIntegrationEvent>
{
    private readonly CommandBus _commandBus;

    public MarkClientAsInactiveEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ClientMarkedAsDebtorIntegrationEvent @event)
    {
        var command = new MarkClientAsInactive(@event.ClientId);
        _commandBus.Handle(command);
    }
}