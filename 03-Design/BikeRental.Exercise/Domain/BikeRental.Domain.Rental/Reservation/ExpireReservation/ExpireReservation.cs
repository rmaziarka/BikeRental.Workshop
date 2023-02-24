using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation.ExpireReservation;

public class ExpireReservationHandler:ICommandHandler<ExpireReservation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Domain.Rental.Reservation.Reservation> _repository;

    public ExpireReservationHandler(EventBus eventBus, Repository<Domain.Rental.Reservation.Reservation> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    public void Handle(ExpireReservation command)
    {
        var reservation = _repository.Query().First(r => r.Id == command.ReservationId);
        reservation.Expire();
    }
}

public record ExpireReservation (Guid ReservationId, Guid ClientId);