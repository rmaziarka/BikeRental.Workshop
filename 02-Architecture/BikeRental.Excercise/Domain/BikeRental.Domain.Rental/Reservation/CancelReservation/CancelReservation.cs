using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation.CancelReservation;

public class CancelReservationHandler:ICommandHandler<CancelReservation>
{
    private readonly EventBus _eventBus;

    public CancelReservationHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(CancelReservation command)
    {
        // create reservation
        var @event = new ReservationCancelled(
                command.ReservationId, 
                command.ClientId
        );

        _eventBus.Publish(@event);
    }
}

public record CancelReservation (Guid ReservationId, Guid ClientId);