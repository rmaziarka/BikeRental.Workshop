using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Client;

public class Client: ICrudEntity
{
    public Guid Id { get; set; }
    
    public bool IsActive { get; set; }
}