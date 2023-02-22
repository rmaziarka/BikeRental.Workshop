using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFeeAndFinishCalculation;

public class ApplyFeeAndFinishChargeEventHandlers : IEventHandler<RentalFinished>, IEventHandler<RentalFinishedOutsideStation>
{
    private readonly CommandBus _commandBus;

    public ApplyFeeAndFinishChargeEventHandlers(CommandBus commandBus)
    {
        _commandBus = commandBus;
    }
    
    public void Handle(RentalFinished @event)
    {
        // get values from domain service
        var feePriceCurrency = "PLN";
        var feePriceAmount = 30;
        var money = new Money(feePriceCurrency, feePriceAmount);
        var description = "rental finished";
        var feeItem = new FeeItem(money, description, @event.FinishDate);
        
        var command = new ApplyFeeAndFinishCalculation(
            new FeeSourceId(@event.RentalId), 
            new []{feeItem}, 
            @event.FinishDate);
        
        _commandBus.Handle(command);
    }

    public void Handle(RentalFinishedOutsideStation @event)
    {
        // get values from domain service
        var rentalFeePriceCurrency = "PLN";
        var rentalFeePriceAmount = 30;
        var rentalMoney = new Money(rentalFeePriceCurrency, rentalFeePriceAmount);
        var rentalDescription = "rental finished";
        var rentalFeeItem = new FeeItem(rentalMoney, rentalDescription, @event.FinishDate);

        // get values from domain service
        var rentalOutsideStationFeePriceCurrency = "PLN";
        var rentalOutsideStationFeePriceAmount = 15;
        var rentalOutsideStationMoney = new Money(rentalOutsideStationFeePriceCurrency, rentalOutsideStationFeePriceAmount);
        var rentalOutsideStationDescription = "rental outside station";
        var rentalOutsideStationFeeItem = new FeeItem(rentalOutsideStationMoney, rentalOutsideStationDescription, @event.FinishDate);

        var command = new ApplyFeeAndFinishCalculation(
            new FeeSourceId(@event.RentalId), 
            new []{rentalFeeItem, rentalOutsideStationFeeItem}, 
            @event.FinishDate);
        
        _commandBus.Handle(command);
    }
}