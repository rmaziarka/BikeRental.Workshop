using BikeRental.Domain.Rental.Reservation;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Rental;

public class RentalId:SingleValueObject<Guid>
{
    public RentalId(Guid value) : base(value)
    {
    }
}

public class Rental : Entity<RentalId>, IAggregateRoot
{
    public static Rental RentBike(RentalId id, BikeId bikeId, ClientId clientId, RentalBasedOn basedOn)
    {
        var startDate = DateTimeOffset.Now;

        return new Rental(id, bikeId, clientId, startDate, basedOn);
    }

    public void Finish()
    {
        if (IsFinished) throw new DomainError();
        
        _finishDate = DateTimeOffset.Now;

        var @event = new RentalFinished(Id, _clientId, _finishDate.Value);
        AddEvent(@event);
    }

    public void FinishOutsideStation()
    {
        if (IsFinished) throw new DomainError();
        
        _finishDate = DateTimeOffset.Now;
        _finishedOutsideStation = true;

        var @event = new RentalFinishedOutsideStation(Id, _clientId, _finishDate.Value);
        AddEvent(@event);
    }

    private bool IsFinished => _finishDate.HasValue;
    
    
    private ClientId _clientId;

    private BikeId _bikeId;

    public DateTimeOffset StartDate { get; }
    
    private readonly RentalBasedOn _basedOn;
    
    private DateTimeOffset? _finishDate;
    
    private bool _finishedOutsideStation;

    private Rental(RentalId id, BikeId bikeId, ClientId clientId, DateTimeOffset startDate, RentalBasedOn basedOn)
    {
        Id = id;
        _bikeId = bikeId;
        _clientId = clientId;
        StartDate = startDate;
        _basedOn = basedOn;
        
        var @event = new BikeRented(Id, _bikeId, _clientId, StartDate);
        AddEvent(@event);
    }
}


public record BikeRented(RentalId RentalId, BikeId BikeId, ClientId ClientId, DateTimeOffset StartDate);
public record RentalFinished(RentalId RentalId, ClientId ClientId, DateTimeOffset FinishDate);
public record RentalFinishedOutsideStation(RentalId RentalId, ClientId ClientId, DateTimeOffset FinishDate);

public class RentalBasedOn : ValueObject
{
    public ReservationId? ReservationId { get; }

    public RentalBasedOnType Type { get; }
    
    public static RentalBasedOn FromReservation(ReservationId reservationId)
    {
        return new RentalBasedOn(RentalBasedOnType.Reservation, reservationId);
    }

    public static RentalBasedOn FromAdHoc()
    {
        return new RentalBasedOn(RentalBasedOnType.AdHoc);
    }

    private RentalBasedOn(RentalBasedOnType type, ReservationId? reservationId = null)
    {
        Type = type;
        ReservationId = reservationId;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return ReservationId;
    }
}

public enum RentalBasedOnType
{
    Reservation,AdHoc
}