using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client.MarkClientAsActive;


public class MarkClientAsActiveHandler: ICommandHandler<MarkClientAsActive>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Client> _repository;

    public MarkClientAsActiveHandler(EventBus eventBus, Repository<Client> repository)
    {
        _eventBus = eventBus;
        _repository = repository;
    }
    
    public void Handle(MarkClientAsActive command)
    {
        var client = _repository.Query().First(c => c.Id == command.ClientId);
        client.IsActive = true;
        
        var @event = new ClientMarkedAsInactive(
            command.ClientId);
        
        _eventBus.Publish(@event);
    }
}

public record MarkClientAsActive(Guid ClientId);