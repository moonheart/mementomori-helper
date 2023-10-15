using Microsoft.Extensions.DependencyInjection;

namespace MementoMori.Common;

public static class Services
{
    private static IServiceProvider? _serviceProvider;

    public static void Setup(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static T Get<T>()
    {
        return (_serviceProvider ?? throw new NullReferenceException("_serviceProvider is null")).GetService<T>() ?? throw new NullReferenceException($"Service {typeof(T).Name} not found");
    }
}