using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFeeAndFinishCalculation;

public class ApplyFeeAndFinishChargeEventHandlers : IEventHandler<RentalFinished>, IEventHandler<RentalFinishedOutsideStation>
{
    private readonly CommandBus _commandBus;

    public ApplyFeeAndFinishChargeEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(RentalFinished @event)
    {
        var fee = 30; // get fee based on type, from domain service
        
        var command = new ApplyFeeAndFinishCalculation(
            @event.RentalId, 
            @event.ClientId, 
            fee, 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }

    public void Handle(RentalFinishedOutsideStation @event)
    {
        var fee = 40; // get fee based on type, from domain service

        var command = new ApplyFeeAndFinishCalculation(
            @event.RentalId, 
            @event.ClientId, 
            fee, 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }
}