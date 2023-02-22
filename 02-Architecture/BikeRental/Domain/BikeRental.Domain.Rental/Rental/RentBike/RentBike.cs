using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.RentBike;

public class RentBikeHandler: ICommandHandler<RentBike>
{
    private readonly EventBus _eventBus;

    public RentBikeHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(RentBike command)
    {
        // create rental
        // remove bike from available
        
        var @event = new BikeRented(
            command.RentalId, 
            command.BikeId, 
            command.ClientId, 
            DateTimeOffset.Now);

        _eventBus.Publish(@event);
    }
}

public record RentBike (Guid RentalId, Guid BikeId, Guid ClientId);