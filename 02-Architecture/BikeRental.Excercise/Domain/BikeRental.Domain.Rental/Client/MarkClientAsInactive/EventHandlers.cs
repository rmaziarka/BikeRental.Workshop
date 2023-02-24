using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsInactive;

public class MarkClientAsInactiveEventHandlers : IEventHandler<ClientMarkedAsDebtor>
{
    private readonly CommandBus _commandBus;

    public MarkClientAsInactiveEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ClientMarkedAsDebtor @event)
    {
        var command = new MarkClientAsInactive(@event.ClientId);
        _commandBus.Handle(command);
    }
}