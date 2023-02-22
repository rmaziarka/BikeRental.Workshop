using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental.Reservation.CancelReservation;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseCancelReservationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/reservation/cancel",
            (
                [FromServices] CommandBus commandBus,
                [FromBody] CancelReservation cancelReservation

            ) =>
            {
                commandBus.Handle(cancelReservation);
            }
        );
        return endpoints;
    }
}