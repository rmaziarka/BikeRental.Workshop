using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFeeAndFinishCalculation;


public class ApplyFeeAndFinishCalculationHandler: ICommandHandler<ApplyFeeAndFinishCalculation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Fee> _repository;

    public ApplyFeeAndFinishCalculationHandler(EventBus eventBus, Repository<Fee> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    
    public void Handle(ApplyFeeAndFinishCalculation command)
    {
        var fee = _repository.Query().First(f => f.SourceId == command.FeeSourceId);
        
        fee.ApplyFeesAndFinishCalculation(command.FeeItems, command.FinishDate);
    }
}


public record ApplyFeeAndFinishCalculation(FeeSourceId FeeSourceId, IEnumerable<FeeItem> FeeItems, DateTimeOffset FinishDate);