using BikeRental.Domain.Billing;
using BikeRental.Domain.Billing.BillingRates.GetBillingRates;
using BikeRental.Domain.Rental;
using BikeRental.Domain.Shared.Billing.Facades;
using BikeRental.Tech;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();

builder.Services
    .AddSingleton<EventBus>()
    .RegisterAllRentalEventHandlers()
    .RegisterAllBillingEventHandlers();

builder.Services
    .AddSingleton<CommandBus>()
    .RegisterAllRentalCommandHandlers()
    .RegisterAllBillingCommandHandlers();

builder.Services.AddTransient<IGetBillingRates, GetBillingRatesFacade>();

var app = builder.Build();

app.UseRouting();

app.MapGet("/", () => "Hello World!");
    
app.UseEndpoints(endpoints =>
    endpoints.UseRentalEndpoints()
);

app.Run();