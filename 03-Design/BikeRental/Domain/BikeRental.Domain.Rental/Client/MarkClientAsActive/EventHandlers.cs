using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsActive;

public class MarkClientAsActiveEventHandlers : IEventHandler<ClientRemovedFromDebtor>
{
    private readonly CommandBus _commandBus;

    public MarkClientAsActiveEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ClientRemovedFromDebtor @event)
    {
        var command = new MarkClientAsActive(@event.ClientId);
        _commandBus.Handle(command);
    }
}