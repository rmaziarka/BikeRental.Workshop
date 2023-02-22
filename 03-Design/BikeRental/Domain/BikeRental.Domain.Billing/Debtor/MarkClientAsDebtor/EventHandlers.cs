using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.Debtor.MarkClientAsDebtor;

public class MarkClientAsDebtorEventHandlers : IEventHandler<ChargeFailed>
{
    private readonly CommandBus _commandBus;

    public MarkClientAsDebtorEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ChargeFailed @event)
    {
        var command = new MarkClientAsDebtor(@event.ClientId, @event.FeeId);
        _commandBus.Handle(command);
    }
}