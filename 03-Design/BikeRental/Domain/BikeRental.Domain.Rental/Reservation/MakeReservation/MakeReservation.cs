using BikeRental.Tech;

namespace BikeRental.Domain.Rental.Reservation.MakeReservation;

public class MakeReservationHandler:ICommandHandler<MakeReservation>
{
    private readonly EventBus _eventBus;
    private readonly Repository<Domain.Rental.Reservation.Reservation> _repository;
    private readonly Repository<Client.Client> _clientRepository;

    public MakeReservationHandler(EventBus eventBus, 
        Repository<Reservation> repository,
        Repository<Client.Client> clientRepository)
    {
        _eventBus = eventBus;
        _repository = repository;
        _clientRepository = clientRepository;
    }
    public void Handle(MakeReservation command)
    {
        var client = _clientRepository.Query().First(c => c.Id == command.ClientId);
        if (!client.IsActive) throw new ApplicationException();
        
        var reservation = Reservation.MakeReservation(command.ReservationId, command.BikeId, command.ClientId);

        _repository.Create(reservation);
        _repository.Commit();
        
        // remove bike from available
        // start timer 
        
        _eventBus.PublishFromEntity(reservation);
    }
}

public record MakeReservation (ReservationId ReservationId, BikeId BikeId, ClientId ClientId);