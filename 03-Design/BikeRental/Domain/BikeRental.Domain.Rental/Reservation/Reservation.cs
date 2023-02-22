using BikeRental.Domain.Shared.Rental;
using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation;

public class ReservationId:SingleValueObject<Guid>
{
    public ReservationId(Guid value) : base(value)
    {
    }
}

public class Reservation : Entity<ReservationId>, IAggregateRoot
{
    public static Reservation MakeReservation(ReservationId id, BikeId bikeId, ClientId clientId)
    {
        var startDate = DateTimeOffset.Now;
        var plannedExpirationDate = startDate.AddMinutes(15);

        return new Reservation(id, bikeId, clientId, startDate, plannedExpirationDate);
    }

    public void Finish()
    {
        if (CannotBeChanged) throw new DomainError();
        
        _finishDate = DateTimeOffset.Now;

        var @event = new ReservationFinished(Id, _clientId, _finishDate.Value);
        AddEvent(@event);
    }
    
    public void Cancel()
    {
        if (CannotBeChanged) throw new DomainError();
        
        _cancellationDate = DateTimeOffset.Now;

        var @event = new ReservationCancelled(Id, _clientId, _cancellationDate.Value);
        AddEvent(@event);
    }
    
    public void Expire()
    {
        if (CannotBeChanged) throw new DomainError();
        
        _expirationDate = DateTimeOffset.Now;

        var @event = new ReservationExpired(Id, _clientId, _expirationDate.Value);
        AddEvent(@event);
    }

    private bool CannotBeChanged => IsCancelled || IsFinished || IsExpired;
    private bool IsCancelled => _cancellationDate.HasValue;
    private bool IsFinished => _finishDate.HasValue;
    private bool IsExpired => _expirationDate.HasValue;
    
    
    private ClientId _clientId;

    public BikeId BikeId { get; }

    private DateTimeOffset _startDate;

    private DateTimeOffset _plannedExpirationDate;
    
    private DateTimeOffset? _finishDate;
    
    private DateTimeOffset? _cancellationDate;
    
    private DateTimeOffset? _expirationDate;
    
    private Reservation(ReservationId id, BikeId bikeId, ClientId clientId, DateTimeOffset startDate, DateTimeOffset plannedExpirationDate)
    {
        Id = id;
        BikeId = bikeId;
        _clientId = clientId;
        _startDate = startDate;
        _plannedExpirationDate = plannedExpirationDate;

        var @event = new ReservationMade(Id, BikeId, _clientId, _startDate, _plannedExpirationDate);
        AddEvent(@event);
    }
}
