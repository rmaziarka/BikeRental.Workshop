using BikeRental.Domain.Billing;
using BikeRental.Domain.Billing.FeeRates;
using BikeRental.Domain.Rental;
using BikeRental.Domain.Rental.Rental;
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

builder.Services
    .AddSingleton<IntegrationEventBus>()
    .RegisterAllRentalIntegrationEventHandlers()
    .RegisterAllBillingIntegrationEventHandlers();

builder.Services.RegisterFeeItemsCalculator();

// for mocking client state
builder.Services
    .AddFakeClientContext();

// for mocking repositories
builder.Services
    .RegisterRentalRepositories()
    .RegisterBillingRepositories();


var app = builder.Build();

app.SetupRentalRepositoriesData()
    .SetupBillingRepositoriesData();

app.UseRouting();

app.MapGet("/", () => "Hello World!");
    
app.UseEndpoints(endpoints =>
    endpoints.UseRentalEndpoints()
);

app.Run();