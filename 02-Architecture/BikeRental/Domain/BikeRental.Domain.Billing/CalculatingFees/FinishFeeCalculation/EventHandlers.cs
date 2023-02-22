using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.FinishFeeCalculation;

public class FinishFeeCalculationEventsHandler : IEventHandler<ReservationExpired>
{
    private readonly CommandBus _commandBus;
    
    public FinishFeeCalculationEventsHandler(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ReservationExpired @event)
    {
        var fee = 30; // get fee based on type, from domain service
        
        var command = new FinishFeeCalculation(
            @event.ReservationId, 
            @event.ClientId, 
            fee, 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }
}