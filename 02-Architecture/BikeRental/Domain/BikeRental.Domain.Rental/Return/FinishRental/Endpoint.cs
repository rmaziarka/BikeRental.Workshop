using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental.Return.FinishRental;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseFinishRentalEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/finish/",
            (
                [FromServices] CommandBus commandBus,
                [FromBody] FinishRental FinishRental

            ) =>
            {
                commandBus.Handle(FinishRental);
            }
        );
        return endpoints;
    }
}