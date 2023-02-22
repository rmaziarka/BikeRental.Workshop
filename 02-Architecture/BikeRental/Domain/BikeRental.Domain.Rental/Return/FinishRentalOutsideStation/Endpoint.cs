using BikeRental.Tech;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental.Return.FinishRentalOutsideStation;

public static class Endpoint
{
    internal static IEndpointRouteBuilder UseFinishRentalOutsideStationEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/rental/finishoutside/",
            (
                [FromServices] CommandBus commandBus,
                [FromBody] FinishRentalOutsideStation finishRentalOutsideStation

            ) =>
            {
                commandBus.Handle(finishRentalOutsideStation);
            }
        );
        return endpoints;
    }
}