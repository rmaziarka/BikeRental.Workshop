using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace BikeRental.Domain.Rental.Reservation.MakeReservation;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseMakeReservationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/reservation/",
            (
                [FromServices] CommandBus commandBus,
                [FromBody] MakeReservation makeReservation

            ) =>
            {
                commandBus.Handle(makeReservation);
            }
        );
        return endpoints;
    }
}