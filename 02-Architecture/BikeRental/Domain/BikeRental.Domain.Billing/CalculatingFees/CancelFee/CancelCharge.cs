using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.CancelFee;

public class CancelFeeHandler: ICommandHandler<CancelFee>
{
    private readonly EventBus _eventBus;

    public CancelFeeHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(
        CancelFee command
    )
    {
        // retrieve fee based on ChargeSourceId
        var feeId = Guid.NewGuid();
        // cancel charge

        var feeFinishedEvent = new FeeCancelled(
            feeId, 
            command.ClientId,
            command.Date);
        
        _eventBus.Publish(feeFinishedEvent);
    }
}


public record CancelFee(Guid ChargeSourceId, Guid ClientId, DateTimeOffset Date);