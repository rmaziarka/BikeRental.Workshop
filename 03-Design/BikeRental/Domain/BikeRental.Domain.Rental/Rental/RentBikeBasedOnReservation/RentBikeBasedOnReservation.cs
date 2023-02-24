using BikeRental.Domain.Rental.Reservation;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.RentBikeBasedOnReservation;

public class RentBikeBasedOnReservationHandler: ICommandHandler<RentBikeBasedOnReservation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Rental> _repository;
    private readonly Repository<Reservation.Reservation> _reservationRepository;

    public RentBikeBasedOnReservationHandler(EventBus eventBus, 
        Repository<Rental> repository, 
        Repository<Reservation.Reservation> reservationRepository)
    {
        _eventBus = eventBus;
        _repository = repository;
        _reservationRepository = reservationRepository;
    }
    public void Handle(RentBikeBasedOnReservation command)
    {
        var reservation = _reservationRepository.Query().First(r => r.Id == command.ReservationId);
        reservation.Finish();

        var basedOn = RentalBasedOn.FromReservation(reservation.Id);
        
        var rental = Rental.RentBike(
            command.RentalId, 
            reservation.BikeId, 
            command.ClientId,
            basedOn
        );
        
        _repository.Create(rental);
    }
}

public record RentBikeBasedOnReservation (RentalId RentalId, ReservationId ReservationId, ClientId ClientId);