using BikeRental.Domain.Rental.Rental;
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
                [FromServices] ClientContext clientContext, 
                [FromBody] FinishRentalRequest finishRentalRequest

            ) =>
            {
                var command = new FinishRental(
                    new RentalId(finishRentalRequest.RentalId)
                );
                
                commandBus.Handle(command);
            }
        );
        return endpoints;
    }
}

public record FinishRentalRequest(Guid RentalId); 