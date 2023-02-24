using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.CancelFee;

public class CancelChargeIntegrationEventsHandler : IIntegrationEventHandler<ReservationCancelledIntegrationEvent>, IIntegrationEventHandler<ReservationFinishedIntegrationEvent>
{
    private readonly CommandBus _commandBus;

    public CancelChargeIntegrationEventsHandler(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ReservationCancelledIntegrationEvent @event)
    {
        var cancellationReason = "user cancelled reservation";
        var feeCancellation = new FeeCancellation(cancellationReason, @event.CancellationDate);
            
        var command = new CancelFee(
            new FeeSourceId(@event.ReservationId),
            feeCancellation);
        
        _commandBus.Handle(command);
    }
    
    public void Handle(ReservationFinishedIntegrationEvent @event)
    {
        var cancellationReason = "user finished reservation";
        var feeCancellation = new FeeCancellation(cancellationReason, @event.FinishDate);
            
        var command = new CancelFee(
            new FeeSourceId(@event.ReservationId),
            feeCancellation);
        
        _commandBus.Handle(command);
    }
}