using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFee;

public class ApplyFeeEventHandlers : IEventHandler<ReservationMade>, IEventHandler<BikeRented>
{
    private readonly CommandBus _commandBus;

    public ApplyFeeEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ReservationMade @event)
    {
        var fee = 10; // get fee based on type, from domain service
        
        var command = new ApplyFee(
            @event.ReservationId, 
            @event.ClientId, 
            fee, 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }

    public void Handle(BikeRented @event)
    {
        var fee = 20; // get fee based on type, from domain service

        var command = new ApplyFee(
            @event.RentalId, 
            @event.ClientId, 
            fee, 
            DateTimeOffset.Now);
        
        _commandBus.Handle(command);
    }
}