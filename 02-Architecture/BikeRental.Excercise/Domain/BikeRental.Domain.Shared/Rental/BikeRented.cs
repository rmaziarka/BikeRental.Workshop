namespace BikeRental.Domain.Shared.Rental;

public record BikeRented(Guid RentalId, Guid BikeId, Guid ClientId, DateTimeOffset StartDate);