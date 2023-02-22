using Microsoft.Extensions.DependencyInjection;

namespace BikeRental.Tech;

public class ClientContext
{
    public string Name { get; set; }
    
    public Guid Id { get; set; }
}

public static class ClientContextExtensions
{
    public static IServiceCollection AddFakeClientContext(this IServiceCollection services)
    {
        services
            .AddSingleton(
                new ClientContext() { Name = "Jan Kowalski", Id = Guid.NewGuid() }
            );

        return services;
    }
}