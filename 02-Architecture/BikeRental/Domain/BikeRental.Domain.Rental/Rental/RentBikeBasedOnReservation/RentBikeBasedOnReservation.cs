using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental.RentBikeBasedOnReservation;

public class RentBikeBasedOnReservationHandler: ICommandHandler<RentBikeBasedOnReservation>
{
    private readonly EventBus _eventBus;

    public RentBikeBasedOnReservationHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(RentBikeBasedOnReservation command)
    {
        // retrieve reservation
        // mark reservation as finished
        // create rental

        var reservationFinishedEvent = new ReservationFinishedEvent(command.ReservationId, DateTimeOffset.Now);
        
        _eventBus.Publish(reservationFinishedEvent);
        
        var @event = new BikeRented(
            command.RentalId,
            command.BikeId, 
            command.ClientId, 
            DateTimeOffset.Now);

        _eventBus.Publish(@event);
    }
}

public record RentBikeBasedOnReservation (Guid RentalId, Guid BikeId, Guid ReservationId, Guid ClientId);