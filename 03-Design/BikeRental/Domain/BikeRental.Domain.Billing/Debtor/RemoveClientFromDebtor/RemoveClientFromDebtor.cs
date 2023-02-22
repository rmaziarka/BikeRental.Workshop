using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.Debtor.RemoveClientFromDebtor;

public class RemoveClientFromDebtorHandler: ICommandHandler<RemoveClientFromDebtor>
{
    private readonly EventBus _eventBus;

    public RemoveClientFromDebtorHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(RemoveClientFromDebtor command)
    {
        // retrieve client
        // remove fee from it and remove debtor status
        // save
        
        var @event = new ClientRemovedFromDebtor(command.ClientId);
        
        _eventBus.Publish(@event);
    }
}

public record RemoveClientFromDebtor(Guid ClientId)
{
}