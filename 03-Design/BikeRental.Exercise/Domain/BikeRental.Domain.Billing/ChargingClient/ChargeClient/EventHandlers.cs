using BikeRental.Domain.Billing.CalculatingFees;
using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;
using FeeCalculationFinished = BikeRental.Domain.Billing.CalculatingFees.FeeCalculationFinished;

namespace BikeRental.Domain.Billing.ChargingClient.ChargeClient;

public class ChargeClientEventsHandler : IEventHandler<FeeCalculationFinished>
{
    private readonly CommandBus _commandBus;

    public ChargeClientEventsHandler(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(FeeCalculationFinished @event)
    {
        // TODO: it shouldn't charge right away, instead it should start a new saga
        
        var command = new ChargeClient(
            @event.FeeId, 
            @event.ClientId, 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }
}