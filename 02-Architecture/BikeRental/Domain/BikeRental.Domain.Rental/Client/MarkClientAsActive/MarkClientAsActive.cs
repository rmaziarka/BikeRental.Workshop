using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsActive;


public class MarkClientAsActiveHandler: ICommandHandler<MarkClientAsActive>
{
    private readonly EventBus _eventBus;

    public MarkClientAsActiveHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(
        MarkClientAsActive command
    )
    {
        // mark client as inactive in database
        
        var @event = new ClientMarkedAsInactive(
            command.ClientId);
        
        _eventBus.Publish(@event);
    }
}

public record MarkClientAsActive(Guid ClientId);