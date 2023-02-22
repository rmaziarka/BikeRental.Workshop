using BikeRental.Domain.Rental.Reservation;
using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental.Rental.CancelReservation;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseCancelReservationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/reservation/cancel",
            (
                [FromServices] CommandBus commandBus,
                [FromBody] CancelReservationRequest request
            ) =>
            {
                var command = new CancelReservation(
                    new ReservationId(request.ReservationId)
                );
                commandBus.Handle(command);
            }
        );
        return endpoints;
    }
}

public record CancelReservationRequest (Guid ReservationId);