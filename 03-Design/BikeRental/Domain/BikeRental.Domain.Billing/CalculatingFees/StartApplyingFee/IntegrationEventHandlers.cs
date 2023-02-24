using BikeRental.Domain.Billing.FeeRates;
using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.StartApplyingFee;

public class ApplyFeeIntegrationEventHandlers : 
    IIntegrationEventHandler<ReservationMadeIntegrationEvent>, 
    IIntegrationEventHandler<BikeRentedIntegrationEvent>
{
    private readonly CommandBus _commandBus;
    private readonly FeeItemsCalculator _feeItemsCalculator;

    public ApplyFeeIntegrationEventHandlers(CommandBus commandBus, FeeItemsCalculator feeItemsCalculator)
    {
        _commandBus = commandBus;
        _feeItemsCalculator = feeItemsCalculator;
    }
    
    public void Handle(ReservationMadeIntegrationEvent @event)
    {
        var feeItems = _feeItemsCalculator.GetFromEvent(@event);

        var feeId = Guid.NewGuid();
        
        var command = new StartApplyingFee(
            new FeeId(feeId),
            new ClientId(@event.ClientId), 
            new FeeSourceId(@event.ReservationId), 
            FeeSourceType.Reservation,
            feeItems, 
            @event.StartDate);
        
        _commandBus.Handle(command);
    }

    public void Handle(BikeRentedIntegrationEvent @event)
    {
        var feeItems = _feeItemsCalculator.GetFromEvent(@event);

        var feeId = Guid.NewGuid();
        
        var command = new StartApplyingFee(
            new FeeId(feeId),
            new ClientId(@event.ClientId), 
            new FeeSourceId(@event.RentalId), 
            FeeSourceType.Rental,
            feeItems, 
            @event.StartDate);
        
        _commandBus.Handle(command);
    }
}