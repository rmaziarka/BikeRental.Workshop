using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsInactive;


public class MarkClientAsInactiveHandler: ICommandHandler<MarkClientAsInactive>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Client> _repository;

    public MarkClientAsInactiveHandler(EventBus eventBus, Repository<Client> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    
    public void Handle(MarkClientAsInactive command)
    {
        var client = _repository.Query().First(c => c.Id == command.ClientId);
        client.IsActive = false;
        
        var @event = new ClientMarkedAsInactive(
            command.ClientId);
        
        _eventBus.Publish(@event);
    }
}

public record MarkClientAsInactive(Guid ClientId);