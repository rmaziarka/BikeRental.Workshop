using BikeRental.Domain.Billing.FeeRates;
using BikeRental.Domain.Rental.Shared.IntegrationEvents;
using BikeRental.Tech;

namespace BikeRental.Domain.Billing.CalculatingFees.ApplyFeeAndFinishCalculation;

public class ApplyFeeAndFinishChargeEventHandlers 

{
    private readonly CommandBus _commandBus;
    private readonly FeeItemsCalculator _feeItemsCalculator;

    public ApplyFeeAndFinishChargeEventHandlers(CommandBus commandBus, FeeItemsCalculator feeItemsCalculator)
    {
        _commandBus = commandBus;
        _feeItemsCalculator = feeItemsCalculator;
    }
}