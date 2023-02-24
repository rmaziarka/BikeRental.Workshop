using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRental.Tech;

// do not even think to use it in production
public class Repository<T>
{
    private List<T> _list = new List<T>();

    public void Create(T obj)
    {
        _list.Add(obj);
        
        var id = obj.GetType().GetProperty("Id").GetValue(obj, null);
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {typeof(T).Name} was added to 'database' with Id: {id}");
    }
    
    public IQueryable<T> Query()
    {
        return _list.AsQueryable();
    }
}


public static class RepositoryExtensions
{
    public static IServiceCollection RegisterAllRepositories(this IServiceCollection services, Assembly assembly)
    {
        
        var repositoryObjectTypes = assembly
            .GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i == typeof(IAggregateRoot) || i == typeof(ICrudEntity)))
            .ToList();

        foreach (var repositoryObjectType in repositoryObjectTypes)
        {
            var repositoryType = typeof(Repository<>)
                .MakeGenericType(repositoryObjectType);

            services.AddSingleton(repositoryType);
        }

        return services;
    }
    
}