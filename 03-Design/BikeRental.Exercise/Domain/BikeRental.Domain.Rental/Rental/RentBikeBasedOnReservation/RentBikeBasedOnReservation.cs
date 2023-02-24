using BikeRental.Domain.Rental.Reservation;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.RentBikeBasedOnReservation;

public class RentBikeBasedOnReservationHandler: ICommandHandler<RentBikeBasedOnReservation>
{
    private readonly EventBus _eventBus;
    //private readonly Repository<Rental> _repository;
    private readonly Repository<Reservation.Reservation> _reservationRepository;

    public RentBikeBasedOnReservationHandler(EventBus eventBus, 
        Repository<Reservation.Reservation> reservationRepository)
    {
        _eventBus = eventBus;
        _reservationRepository = reservationRepository;
    }
    public void Handle(RentBikeBasedOnReservation command)
    {
        var reservation = _reservationRepository.Query().First(r => r.Id == command.ReservationId);
        reservation.Finish();

        // create rental
        // save rental in repo
    }
}

public record RentBikeBasedOnReservation (Guid RentalId, ReservationId ReservationId, ClientId ClientId);