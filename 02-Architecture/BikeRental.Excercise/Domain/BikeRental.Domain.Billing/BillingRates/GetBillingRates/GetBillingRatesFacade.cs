using BikeRental.Domain.Shared.Billing.Facades;

namespace BikeRental.Domain.Billing.BillingRates.GetBillingRates;

public class GetBillingRatesFacade: IGetBillingRates
{
    public IEnumerable<BillingRatesDto> Get()
    {
        return new[]
        {
            new BillingRatesDto() { BillingRateName = "ReservationMade", BillingRatePrice = 5 },
            new BillingRatesDto() { BillingRateName = "RentalStarted", BillingRatePrice = 10 },
            new BillingRatesDto() { BillingRateName = "RentalFinishedOutsideStation", BillingRatePrice = 15 }
        };
    }
}