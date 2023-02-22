using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.CancelFee;

public class CancelChargeEventsHandler : IEventHandler<ReservationCancelled>
{
    private readonly CommandBus _commandBus;

    public CancelChargeEventsHandler(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ReservationCancelled @event)
    {
        var command = new CancelFee(
            @event.ReservationId, 
            @event.ClientId, 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }
}