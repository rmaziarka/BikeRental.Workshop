using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.Debtor.RemoveClientFromDebtor;

public class RemoveClientFromDebtorEventHandlers : IEventHandler<ChargeSucceeded>
{
    private readonly CommandBus _commandBus;

    public RemoveClientFromDebtorEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ChargeSucceeded @event)
    {
        var command = new RemoveClientFromDebtor(@event.ClientId);
        _commandBus.Handle(command);
    }
}