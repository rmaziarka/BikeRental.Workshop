using BikeRental.Domain.Shared.Billing;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.ChargingClient.ChargeClient;


public class ChargeClientHandler: ICommandHandler<ChargeClient>
{
    private readonly EventBus _eventBus;

    public ChargeClientHandler(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    
    public void Handle(ChargeClient command)
    {
        // retrieve fee
        // charge client
        // some error handling

        var isSuccess = false;

        if (isSuccess)
        {
            var @event = new ChargeSucceeded(command.FeeId, command.ClientId, command.Date);
            _eventBus.Publish(@event);
        }
        else
        {
            var @event = new ChargeFailed(command.FeeId, command.ClientId, command.Date);
            _eventBus.Publish(@event);
        }
    }
}


public record ChargeClient(Guid FeeId, Guid ClientId, DateTimeOffset Date);