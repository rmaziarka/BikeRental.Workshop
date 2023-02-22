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
        var cancellationReason = "user cancelled reservation";
        var feeCancellation = new FeeCancellation(cancellationReason, @event.CancellationDate);
            
        var command = new CancelFee(
            new FeeSourceId(@event.ReservationId),
            feeCancellation);
        
        _commandBus.Handle(command);
    }
}