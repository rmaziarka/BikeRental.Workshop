using BikeRental.Domain.Shared.Billing;
using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.StartApplyingFee;

public class ApplyFeeEventHandlers : IEventHandler<ReservationMade>, IEventHandler<BikeRented>
{
    private readonly CommandBus _commandBus;

    public ApplyFeeEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(ReservationMade @event)
    {
        var applyDate = @event.StartDate;
        
        // get values from domain service
        var feePriceCurrency = "PLN";
        var feePriceAmount = 10;
        var money = new Money(feePriceCurrency, feePriceAmount);
        var description = "reservation made";
        var feeItem = new FeeItem(money, description, DateTimeOffset.Now);

        var feeId = Guid.NewGuid();
        
        var command = new StartApplyingFee(
            new FeeId(feeId),
            new ClientId(@event.ClientId), 
            new FeeSourceId(@event.ReservationId), 
            FeeSourceType.Reservation,
            new []{feeItem}, 
            applyDate);
        
        _commandBus.Handle(command);
    }

    public void Handle(BikeRented @event)
    {
        var applyDate = @event.StartDate;
        
        // get values from domain service
        var feePriceCurrency = "PLN";
        var feePriceAmount = 10;
        var money = new Money(feePriceCurrency, feePriceAmount);
        var description = "bike rented";
        var feeItem = new FeeItem(money, description, applyDate);
        
        var feeId = Guid.NewGuid();
        
        var command = new StartApplyingFee(
            new FeeId(feeId),
            new ClientId(@event.ClientId), 
            new FeeSourceId(@event.RentalId), 
            FeeSourceType.Rental,
            new []{feeItem}, 
            applyDate);
        
        _commandBus.Handle(command);
    }
}