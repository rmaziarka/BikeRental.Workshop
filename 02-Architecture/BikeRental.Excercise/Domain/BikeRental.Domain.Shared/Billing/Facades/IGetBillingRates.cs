namespace BikeRental.Domain.Shared.Billing.Facades;

public interface IGetBillingRates
{
    IEnumerable<BillingRatesDto> Get();
}

public record BillingRatesDto
{
    public string BillingRateName { get; set; }
    
    public decimal BillingRatePrice { get; set; }
}