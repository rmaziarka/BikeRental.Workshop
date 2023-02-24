namespace BikeRental.Domain.Shared.Rental;

public record RentalFinished(Guid RentalId, Guid ClientId, DateTimeOffset RentalFinishDate);