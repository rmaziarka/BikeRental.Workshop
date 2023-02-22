using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFeeAndFinishCalculation;


public class ApplyFeeAndFinishCalculationHandler: ICommandHandler<ApplyFeeAndFinishCalculation>
{
    private readonly EventBus _eventBus;

    public ApplyFeeAndFinishCalculationHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(
        ApplyFeeAndFinishCalculation command
    )
    {
        // retrieve or create new fee based on ChargeSourceId
        var feeId = Guid.NewGuid();
        // apply fee and finish
        
        var @event = new FeeApplied(
            feeId,
            command.ClientId, 
            command.Fee,
            command.ChargeSourceId,
            command.Date);
        
        _eventBus.Publish(@event);
        
        var feeFinishedEvent = new FeeCalculationFinished(
            feeId, 
            command.ClientId, 
            command.Date);
        
        _eventBus.Publish(feeFinishedEvent);
    }
}


public record ApplyFeeAndFinishCalculation(Guid ChargeSourceId, Guid ClientId, decimal Fee, DateTimeOffset Date);