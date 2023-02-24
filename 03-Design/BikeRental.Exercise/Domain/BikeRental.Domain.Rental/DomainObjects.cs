using BikeRental.Tech;

namespace BikeRental.Domain.Rental;


public class BikeId:SingleValueObject<Guid>
{
    public BikeId(Guid value) : base(value)
    {
    }
}

public class ClientId:SingleValueObject<Guid>
{
    public ClientId(Guid value) : base(value)
    {
    }
}