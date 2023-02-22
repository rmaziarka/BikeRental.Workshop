using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.ExpireReservation;

public class ExpireReservationHandler:ICommandHandler<ExpireReservation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Reservation.Reservation> _repository;

    public ExpireReservationHandler(EventBus eventBus, Repository<Reservation.Reservation> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    public void Handle(ExpireReservation command)
    {
        var reservation = _repository.Query().First(r => r.Id == command.ReservationId);
        reservation.Expire();
        
        _repository.Commit();
        
        _eventBus.Publish(reservation.Events);
    }
}

public record ExpireReservation (Guid ReservationId, Guid ClientId);