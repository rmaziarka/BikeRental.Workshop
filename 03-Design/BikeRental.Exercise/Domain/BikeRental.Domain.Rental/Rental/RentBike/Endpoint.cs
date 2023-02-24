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
                [FromServices] ClientContext clientContext, 
                [FromBody] RentBikeRequest rentBikeRequest

            ) =>
            {
                var rentalId = Guid.NewGuid();
                
                var command = new RentBike(
                    rentalId,
                    new BikeId(rentBikeRequest.BikeId),
                    new ClientId(clientContext.Id)
                );
                
                commandBus.Handle(command);

                return rentalId;
            }
        );
        return endpoints;
    }
}

public record RentBikeRequest (Guid RentalId, Guid BikeId, Guid ClientId);