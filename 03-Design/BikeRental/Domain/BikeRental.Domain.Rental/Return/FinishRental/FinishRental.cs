using BikeRental.Domain.Rental.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Return.FinishRental;

public class FinishRentalHandler:ICommandHandler<FinishRental>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Rental.Rental> _repository;

    public FinishRentalHandler(EventBus eventBus, Repository<Rental.Rental> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    public void Handle(FinishRental command)
    {
        var rental = _repository.Query().First(r => r.Id == command.RentalId);
        rental.Finish();
    }
}

public record FinishRental (RentalId RentalId);