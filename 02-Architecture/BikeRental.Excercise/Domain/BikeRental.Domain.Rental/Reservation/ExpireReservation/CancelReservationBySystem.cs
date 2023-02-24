using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation.ExpireReservation;

public class ExpireReservationHandler:ICommandHandler<ExpireReservation>
{
    private readonly EventBus _eventBus;

    public ExpireReservationHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(ExpireReservation command)
    {
        // create reservation
        var @event = new ReservationExpired(
                command.ReservationId, 
                command.ClientId
        );

        _eventBus.Publish(@event);
    }
}

public record ExpireReservation (Guid ReservationId, Guid ClientId);