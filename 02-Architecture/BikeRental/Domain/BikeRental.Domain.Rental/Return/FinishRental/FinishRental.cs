using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Return.FinishRental;

public class FinishRentalHandler:ICommandHandler<FinishRental>
{
    private readonly EventBus _eventBus;

    public FinishRentalHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(FinishRental command)
    {
        // retrieve rental
        // mark rental as finished
        // return bike to available
        
        var @event = new RentalFinished(
            command.RentalId, 
            command.ClientId, 
            DateTimeOffset.Now);

        _eventBus.Publish(@event);
    }
}

public record FinishRental (Guid RentalId, Guid ClientId);