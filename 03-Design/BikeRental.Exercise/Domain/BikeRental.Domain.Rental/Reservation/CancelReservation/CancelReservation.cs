using BikeRental.Domain.Rental.Reservation;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.CancelReservation;

public class CancelReservationHandler:ICommandHandler<CancelReservation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Reservation.Reservation> _repository;

    public CancelReservationHandler(EventBus eventBus, Repository<Reservation.Reservation> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    public void Handle(CancelReservation command)
    {
        var reservation = _repository.Query().First(r => r.Id == command.ReservationId);
        
        reservation.Cancel();
    }
}

public record CancelReservation (ReservationId ReservationId);