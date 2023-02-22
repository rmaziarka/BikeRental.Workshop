using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.RentBike;

public class RentBikeHandler: ICommandHandler<RentBike>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Rental> _repository;

    public RentBikeHandler(EventBus eventBus, Repository<Rental> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    public void Handle(RentBike command)
    {
        var rental = Rental.RentBike(command.RentalId, command.BikeId, command.ClientId);
        _repository.Create(rental);
        
        // remove bike availability
        
        _repository.Commit();

        _eventBus.PublishFromEntity(rental);
    }
}

public record RentBike (RentalId RentalId, BikeId BikeId, ClientId ClientId);