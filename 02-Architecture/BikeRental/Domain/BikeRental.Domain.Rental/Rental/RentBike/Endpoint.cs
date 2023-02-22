using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental.Rental.RentBike;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseRentBikeEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/rental/",
            (
                [FromServices] CommandBus commandBus,
                [FromBody] RentBike rentBike

            ) =>
            {
                commandBus.Handle(rentBike);
            }
        );
        return endpoints;
    }
}