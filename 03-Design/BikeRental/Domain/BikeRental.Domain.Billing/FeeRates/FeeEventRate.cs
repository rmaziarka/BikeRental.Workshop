using BikeRental.Tech;

namespace BikeRental.Domain.Billing.FeeRates;

public class EventRate:ICrudEntity
{
    public Guid Id { get; set; }
    
    public string EventName { get; set; }

    public IEnumerable<FeeRate> FeeRates { get; set; }
}

public class FeeRate
{
    public string Currency { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Description { get; set; }
}