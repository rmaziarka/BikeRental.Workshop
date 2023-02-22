using BikeRental.Domain.Rental.Reservation;
using BikeRental.Domain.Shared.Rental;
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
    public static Rental RentBike(RentalId id, BikeId bikeId, ClientId clientId)
    {
        var startDate = DateTimeOffset.Now;

        return new Rental(id, bikeId, clientId, startDate);
    }
    
    public static Rental RentBikeFromReservation(RentalId id, BikeId bikeId, ClientId clientId, ReservationId reservationId)
    {
        var startDate = DateTimeOffset.Now;

        return new Rental(id, bikeId, clientId, startDate, reservationId);
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

    private DateTimeOffset _startDate;
    
    private readonly RentalBasedOn _basedOn;
    
    private DateTimeOffset? _finishDate;
    
    private bool _finishedOutsideStation;

    private Rental(RentalId id, BikeId bikeId, ClientId clientId, DateTimeOffset startDate)
    {
        Id = id;
        _bikeId = bikeId;
        _clientId = clientId;
        _startDate = startDate;
        _basedOn = RentalBasedOn.FromAdHoc();
        
        var @event = new BikeRented(Id, _bikeId, _clientId, _startDate);
        AddEvent(@event);
    }
    
    private Rental(RentalId id, BikeId bikeId, ClientId clientId, DateTimeOffset startDate, ReservationId reservationId)
    {
        Id = id;
        _bikeId = bikeId;
        _clientId = clientId;
        _startDate = startDate;
        _basedOn = RentalBasedOn.FromReservation(reservationId);
        
        var @event = new BikeRented(Id, _bikeId, _clientId, _startDate);
        AddEvent(@event);
    }
}

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
        throw new NotImplementedException();
    }
}

public enum RentalBasedOnType
{
    Reservation,AdHoc
}