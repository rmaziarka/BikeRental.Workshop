using BikeRental.Domain.Rental.Rental;
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
                [FromServices] ClientContext clientContext, 
                [FromBody] FinishRentalOutsideStationRequest finishRentalOutsideStationRequest

            ) =>
            {
                var command = new FinishRentalOutsideStation(
                    new RentalId(finishRentalOutsideStationRequest.RentalId)
                );
                
                commandBus.Handle(command);
            }
        );
        return endpoints;
    }
}

public record FinishRentalOutsideStationRequest(Guid RentalId); 