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
                [FromBody] RentBikeBasedOnReservation RentBikeBasedOnReservation

            ) =>
            {
                commandBus.Handle(RentBikeBasedOnReservation);
            }
        );
        return endpoints;
    }
}