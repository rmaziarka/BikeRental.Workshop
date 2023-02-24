using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation;

public class IntegrationEventsPublisher:IEventHandler<ReservationMade>
    ,IEventHandler<ReservationFinished>
    ,IEventHandler<ReservationCancelled>
    ,IEventHandler<ReservationExpired>
{
    private readonly IntegrationEventBus _integrationEventBus;

    public IntegrationEventsPublisher(IntegrationEventBus integrationEventBus)
    {
        _integrationEventBus = integrationEventBus;
    }
    
    public void Handle(ReservationMade @event)
    {
        var integrationEventEvent = new ReservationMadeIntegrationEvent(
            @event.ReservationId, @event.BikeId, @event.ClientId,
            @event.StartDate, @event.ExpirationDate);
        
        _integrationEventBus.Publish(integrationEventEvent);
    }

    public void Handle(ReservationFinished @event)
    {
        var integrationEventEvent = new ReservationFinishedIntegrationEvent(
            @event.ReservationId, @event.ClientId, @event.FinishDate);
        
        _integrationEventBus.Publish(integrationEventEvent);
    }

    public void Handle(ReservationCancelled @event)
    {
        var integrationEventEvent = new ReservationCancelledIntegrationEvent(
            @event.ReservationId, @event.ClientId, @event.CancellationDate);
        
        _integrationEventBus.Publish(integrationEventEvent);
    }

    public void Handle(ReservationExpired @event)
    {
        var integrationEventEvent = new ReservationExpiredIntegrationEvent(
            @event.ReservationId, @event.ClientId, @event.ExpirationDate);
        
        _integrationEventBus.Publish(integrationEventEvent);
    }
}