using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.StartApplyingFee;


public class StartApplyingFeeHandler: ICommandHandler<StartApplyingFee>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Fee> _repository;

    public StartApplyingFeeHandler(EventBus eventBus, Repository<Fee> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    
    public void Handle(StartApplyingFee command)
    {
        var fee = Fee.StartFee(
            command.FeeId, 
            command.ClientId, 
            command.FeeSourceType, 
            command.FeeSourceId,
            command.ApplyDate);
        
        fee.ApplyFees(command.FeeItems);
        
        _repository.Create(fee);
    }
}

public record StartApplyingFee(
    FeeId FeeId, 
    ClientId ClientId, 
    FeeSourceId FeeSourceId, 
    FeeSourceType FeeSourceType,
    IEnumerable<FeeItem> FeeItems, 
    DateTimeOffset ApplyDate);