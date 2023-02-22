using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation.ExpireReservation;

public class ReservationTimerPassedHandler : IEventHandler<ReservationTimerPassed>
{
    private readonly CommandBus _commandBus;

    public ReservationTimerPassedHandler(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ReservationTimerPassed @event)
    {
        var command = new Rental.ExpireReservation.ExpireReservation(@event.ReservationId, @event.ClientId);
        _commandBus.Handle(command);
    }
}

public record ReservationTimerPassed(Guid ReservationId, Guid ClientId);