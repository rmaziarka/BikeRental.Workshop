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
        var command = new FinishFeeCalculation(
            new FeeSourceId(@event.ReservationId), 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }
}