using BikeRental.Domain.Rental.Reservation;
using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace BikeRental.Domain.Rental.Rental.MakeReservation;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseMakeReservationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/reservation/",
            (
                [FromServices] CommandBus commandBus,
                [FromServices] ClientContext clientContext, 
                [FromBody] MakeReservationRequest makeReservationRequest

            ) =>
            {
                var reservationId = Guid.NewGuid();
                
                var command = new Reservation.MakeReservation.MakeReservation(
                    new ReservationId(reservationId),
                    new BikeId(makeReservationRequest.BikeId),
                    new ClientId(clientContext.Id)
                );
                
                commandBus.Handle(command);

                return reservationId;
            }
        );
        return endpoints;
    }
}

public record MakeReservationRequest(Guid ReservationId, Guid BikeId, Guid ClientId);