using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.CancelFee;

public class CancelFeeHandler: ICommandHandler<CancelFee>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Fee> _repository;

    public CancelFeeHandler(EventBus eventBus, Repository<Fee> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    
    public void Handle(CancelFee command)
    {
        var fee = _repository.Query().First(f => f.SourceId == command.FeeSourceId);
        
        fee.Cancel(command.FeeCancellation);
        
        _repository.Commit();
        
        _eventBus.PublishFromEntity(fee);
    }
}

public record CancelFee(FeeSourceId FeeSourceId, FeeCancellation FeeCancellation);