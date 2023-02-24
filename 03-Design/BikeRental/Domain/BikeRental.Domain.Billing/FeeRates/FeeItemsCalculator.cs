using System.Reflection;
using BikeRental.Domain.Billing.CalculatingFees;
using BikeRental.Tech;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRental.Domain.Billing.FeeRates;

public class FeeItemsCalculator
{
    private IEnumerable<EventRate> _eventRates;

    public FeeItemsCalculator(IEnumerable<EventRate> eventRates)
    {
        _eventRates = eventRates;
    }

    public IEnumerable<FeeItem> GetFromEvent(object @event)
    {
        var eventRate = _eventRates.First(r => r.EventName == @event.GetType().Name);
        var now = DateTimeOffset.Now;

        return eventRate.FeeRates.Select(fr => new FeeItem(new Money(fr.Currency, fr.Amount), fr.Description, now));
    }
}

public static class FeeItemsCalculatorExtensions
{
    public static IServiceCollection RegisterFeeItemsCalculator(this IServiceCollection services)
    {
        services.AddTransient<FeeItemsCalculator>(provider =>
        {
            var repo = provider.GetService<Repository<EventRate>>();

            var eventRates = repo.Query().ToList();

            return new FeeItemsCalculator(eventRates);
        });
        
        return services;
    }
}