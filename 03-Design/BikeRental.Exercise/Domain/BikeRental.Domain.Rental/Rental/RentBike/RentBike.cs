using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.RentBike;

public class RentBikeHandler: ICommandHandler<RentBike>
{
    private readonly EventBus _eventBus;
    //private readonly Repository<Rental> _repository;

    public RentBikeHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(RentBike command)
    {
        // create rental
        // save rental in repo
    }
}

public record RentBike (Guid RentalId, BikeId BikeId, ClientId ClientId);