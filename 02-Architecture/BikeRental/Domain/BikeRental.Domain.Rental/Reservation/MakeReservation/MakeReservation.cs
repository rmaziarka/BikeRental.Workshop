using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation.MakeReservation;

public class MakeReservationHandler:ICommandHandler<MakeReservation>
{
    private readonly EventBus _eventBus;

    public MakeReservationHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    public void Handle(MakeReservation command)
    {
        // create reservation
        // remove bike from available
        // start timer 
        
        var @event = new ReservationMade(
            command.ReservationId, 
            command.BikeId, 
            command.ClientId, 
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddMinutes(15));

        _eventBus.Publish(@event);
    }
}

public record MakeReservation (Guid ReservationId, Guid BikeId, Guid ClientId);