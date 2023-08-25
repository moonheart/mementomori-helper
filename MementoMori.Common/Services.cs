using Microsoft.Extensions.DependencyInjection;

namespace MementoMori.Common;

public static class Services
{
    private static IServiceProvider _serviceProvider;

    public static void Setup(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public static T Get<T>() => _serviceProvider.GetService<T>()!;
}