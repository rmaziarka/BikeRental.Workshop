using BikeRental.Tech;

public class FeeSourceId:SingleValueObject<Guid>
{
    public FeeSourceId(Guid value) : base(value)
    {
    }
}

public class ClientId:SingleValueObject<Guid>
{
    public ClientId(Guid value) : base(value)
    {
    }
}


public class Money: ValueObject
{
    public Money(string currency, decimal amount)
    {
        if (String.IsNullOrEmpty(currency)) throw new ArgumentException();
        if (amount < 0) throw new ArgumentException();
        
        Currency = currency;
        Amount = amount;
    }

    public string Currency { get;  }
    
    public decimal Amount { get;  }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Currency;
        yield return Amount;
    }
}