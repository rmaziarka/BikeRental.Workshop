namespace BikeRental.Domain.Shared.Rental;

public record RentalFinishedOutsideStation(Guid RentalId, Guid ClientId, DateTimeOffset FinishDate);