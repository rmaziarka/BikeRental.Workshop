using BikeRental.Domain.Rental.Rental;
using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Return.FinishRentalOutsideStation;

public class FinishRentalOutsideStationHandler:ICommandHandler<FinishRentalOutsideStation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Rental.Rental> _repository;

    public FinishRentalOutsideStationHandler(EventBus eventBus, Repository<Rental.Rental> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    public void Handle(FinishRentalOutsideStation command)
    {
        var rental = _repository.Query().First(r => r.Id == command.RentalId);
        rental.FinishOutsideStation();

        _eventBus.PublishFromEntity(rental);
    }
}

public record FinishRentalOutsideStation (RentalId RentalId);