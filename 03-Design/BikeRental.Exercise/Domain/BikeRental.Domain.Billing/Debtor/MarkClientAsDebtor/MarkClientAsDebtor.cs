using BikeRental.Domain.Billing.Shared.IntegrationEvents;
using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.Debtor.MarkClientAsDebtor;

public class MarkClientAsDebtorHandler: ICommandHandler<MarkClientAsDebtor>
{
    private readonly EventBus _eventBus;

    public MarkClientAsDebtorHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(MarkClientAsDebtor command)
    {
        // retrieve client
        // attach fee to it and mark as debtor
        // save
        
        var @event = new ClientMarkedAsDebtorIntegrationEvent(command.ClientId);
        
        _eventBus.Publish(@event);
    }
}

public record MarkClientAsDebtor(Guid ClientId, Guid FeeId)
{
}