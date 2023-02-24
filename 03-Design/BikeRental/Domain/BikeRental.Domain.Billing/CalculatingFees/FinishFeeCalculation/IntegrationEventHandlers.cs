using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.FinishFeeCalculation;

public class FinishFeeCalculationIntegrationEventsHandler : IIntegrationEventHandler<ReservationExpiredIntegrationEvent>
{
    private readonly CommandBus _commandBus;
    
    public FinishFeeCalculationIntegrationEventsHandler(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ReservationExpiredIntegrationEvent @event)
    {
        var command = new FinishFeeCalculation(
            new FeeSourceId(@event.ReservationId), 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }
}