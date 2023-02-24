using BikeRental.Domain.Billing.FeeRates;
using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFeeAndFinishCalculation;

public class ApplyFeeAndFinishChargeEventHandlers : 
    IIntegrationEventHandler<RentalFinishedIntegrationEvent>, 
    IIntegrationEventHandler<RentalFinishedOutsideStationIntegrationEvent>
{
    private readonly CommandBus _commandBus;
    private readonly FeeItemsCalculator _feeItemsCalculator;

    public ApplyFeeAndFinishChargeEventHandlers(CommandBus commandBus, FeeItemsCalculator feeItemsCalculator)
    {
        _commandBus = commandBus;
        _feeItemsCalculator = feeItemsCalculator;
    }
    
    public void Handle(RentalFinishedIntegrationEvent @event)
    {
        var feeItems = _feeItemsCalculator.GetFromEvent(@event);
        
        var command = new ApplyFeeAndFinishCalculation(
            new FeeSourceId(@event.RentalId), 
            feeItems, 
            @event.FinishDate);
        
        _commandBus.Handle(command);
    }

    public void Handle(RentalFinishedOutsideStationIntegrationEvent @event)
    {
        var feeItems = _feeItemsCalculator.GetFromEvent(@event);

        var command = new ApplyFeeAndFinishCalculation(
            new FeeSourceId(@event.RentalId), 
            feeItems,
            @event.FinishDate);
        
        _commandBus.Handle(command);
    }
}