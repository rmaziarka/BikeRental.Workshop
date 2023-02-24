using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental;

public class IntegrationEventPublisher:IEventHandler<BikeRented>
    ,IEventHandler<RentalFinished>
    ,IEventHandler<RentalFinishedOutsideStation>

{
    private readonly IntegrationEventBus _integrationEventBus;
    private readonly Repository<Rental> _repository;

    public IntegrationEventPublisher(IntegrationEventBus integrationEventBus, Repository<Rental> repository)
    {
        _integrationEventBus = integrationEventBus;
        _repository = repository;
    }
    
    public void Handle(BikeRented @event)
    {
        var integrationEventEvent = new BikeRentedIntegrationEvent(
            @event.RentalId, @event.BikeId, @event.ClientId,
            @event.StartDate);
        
        _integrationEventBus.Publish(integrationEventEvent);
    }

    public void Handle(RentalFinished @event)
    {
        var rental = _repository.Query().First(r => r.Id == @event.RentalId);
        
        var integrationEventEvent = new RentalFinishedIntegrationEvent(
            @event.RentalId, @event.ClientId, rental.StartDate, @event.FinishDate);
        
        _integrationEventBus.Publish(integrationEventEvent);
    }

    public void Handle(RentalFinishedOutsideStation @event)
    {
        var rental = _repository.Query().First(r => r.Id == @event.RentalId);
        
        var integrationEventEvent = new RentalFinishedOutsideStationIntegrationEvent(
            @event.RentalId, @event.ClientId, rental.StartDate, @event.FinishDate);
        
        _integrationEventBus.Publish(integrationEventEvent);
    }
}