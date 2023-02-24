using BikeRental.Domain.Rental.Reservation.CancelReservation;
using BikeRental.Domain.Rental.Reservation.MakeReservation;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental;

public static class Endpoints
{
    public static IEndpointRouteBuilder UseRentalEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints
            .UseMakeReservationEndpoint()
            .UseCancelReservationEndpoint();
        
}