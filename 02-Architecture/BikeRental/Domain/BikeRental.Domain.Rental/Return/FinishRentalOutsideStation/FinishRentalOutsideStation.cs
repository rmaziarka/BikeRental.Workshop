using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Return.FinishRentalOutsideStation;

public class FinishRentalOutsideStationHandler:ICommandHandler<FinishRentalOutsideStation>
{
    private readonly EventBus _eventBus;

    public FinishRentalOutsideStationHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(FinishRentalOutsideStation command)
    {
        // retrieve rental
        // mark rental as finished
        // return bike to available
        
        var @event = new RentalFinishedOutsideStation(
            command.RentalId, 
            command.ClientId, 
            DateTimeOffset.Now);

        _eventBus.Publish(@event);
    }
}

public record FinishRentalOutsideStation (Guid RentalId, Guid ClientId);