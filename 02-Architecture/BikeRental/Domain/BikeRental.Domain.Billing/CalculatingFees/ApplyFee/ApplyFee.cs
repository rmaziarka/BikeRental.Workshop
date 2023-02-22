using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFee;


public class ApplyFeeHandler: ICommandHandler<ApplyFee>
{
    private readonly EventBus _eventBus;

    public ApplyFeeHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(
        ApplyFee command
    )
    {
        // retrieve or create new fee based on ChargeSourceId
        var feeId = Guid.NewGuid();
        // apply fee
        
        var @event = new FeeApplied(
            feeId,
            command.ClientId, 
            command.Fee,
            command.ChargeSourceId,
            command.Date);
        
        _eventBus.Publish(@event);
    }
}

public record ApplyFee(Guid ChargeSourceId, Guid ClientId, decimal Fee, DateTimeOffset Date);