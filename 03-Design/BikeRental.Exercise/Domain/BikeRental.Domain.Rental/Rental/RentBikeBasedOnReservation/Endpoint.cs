using BikeRental.Domain.Rental.Reservation;
using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental.Rental.RentBikeBasedOnReservation;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseRentBikeBasedOnReservationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/rental/basedOnReservation",
            (
                [FromServices] CommandBus commandBus,
                [FromServices] ClientContext clientContext, 
                [FromBody] RentBikeBasedOnReservationRequest rentBikeRequest

            ) =>
            {
                var rentalId = Guid.NewGuid();
                
                var command = new RentBikeBasedOnReservation(
                    rentalId,
                    new ReservationId(rentBikeRequest.ReservationId),
                    new ClientId(clientContext.Id)
                );
                
                commandBus.Handle(command);

                return rentalId;
            }
        );
        return endpoints;
    }
}

public record RentBikeBasedOnReservationRequest (Guid ReservationId);