using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.FinishFeeCalculation;


public class FinishFeeCalculationHandler: ICommandHandler<FinishFeeCalculation>
{
    private readonly EventBus _eventBus;

    public FinishFeeCalculationHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(
        FinishFeeCalculation command
    )
    {
        // retrieve charge based on ChargeSourceId
        var chargeId = Guid.NewGuid();
        // finish charge

        var feeFinishedEvent = new FeeCalculationFinished(
            chargeId, 
            command.ClientId, 
            command.Date);
        
        _eventBus.Publish(feeFinishedEvent);
    }
}


public record FinishFeeCalculation(Guid ChargeSourceId, Guid ClientId, decimal Fee, DateTimeOffset Date);