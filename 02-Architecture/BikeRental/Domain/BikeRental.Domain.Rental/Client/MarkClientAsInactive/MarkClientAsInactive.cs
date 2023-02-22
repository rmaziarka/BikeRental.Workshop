using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsInactive;


public class MarkClientAsInactiveHandler: ICommandHandler<MarkClientAsInactive>
{
    private readonly EventBus _eventBus;

    public MarkClientAsInactiveHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(
        MarkClientAsInactive command
    )
    {
        // mark client as inactive in database
        
        var @event = new ClientMarkedAsInactive(
            command.ClientId);
        
        _eventBus.Publish(@event);
    }
}

public record MarkClientAsInactive(Guid ClientId);