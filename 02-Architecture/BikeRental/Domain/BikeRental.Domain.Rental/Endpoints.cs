using BikeRental.Domain.Rental.Rental.RentBike;
using BikeRental.Domain.Rental.Reservation.CancelReservation;
using BikeRental.Domain.Rental.Reservation.MakeReservation;
using BikeRental.Domain.Rental.Return.FinishRental;
using BikeRental.Domain.Rental.Return.FinishRentalOutsideStation;
using Microsoft.AspNetCore.Routing;

namespace BikeRental.Domain.Rental;

public static class Endpoints
{
    public static IEndpointRouteBuilder UseRentalEndpoints(this IEndpointRouteBuilder endpoints) =>
        endpoints
            .UseMakeReservationEndpoint()
            .UseCancelReservationEndpoint()
            .UseRentBikeEndpoint()
            .UseFinishRentalEndpoint()
            .UseFinishRentalOutsideStationEndpoint()
        ;
}