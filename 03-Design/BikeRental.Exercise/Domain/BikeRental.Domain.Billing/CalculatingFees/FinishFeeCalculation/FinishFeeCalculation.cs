using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.FinishFeeCalculation;


public class FinishFeeCalculationHandler: ICommandHandler<FinishFeeCalculation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Fee> _repository;

    public FinishFeeCalculationHandler(EventBus eventBus, Repository<Fee> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    
    public void Handle(FinishFeeCalculation command)
    {
        var fee = _repository.Query().First(f => f.SourceId == command.FeeSourceId);
        
        fee.FinishCalculation(command.FinishDate);
    }
}


public record FinishFeeCalculation(FeeSourceId FeeSourceId, DateTimeOffset FinishDate);