using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace MementoMori.Option;

public static class ServiceCollectionExtensions
{
    public static void ConfigureWritable<T>(
        this IServiceCollection services,
        IConfigurationSection section,
        string file = "appsettings.json",
        IFileProvider? fileProvider = null) where T : class, new()
    {
        services.Configure<T>(section);
        services.AddSingleton<IWritableOptions<T>>(provider =>
        {
            var configuration = (IConfigurationRoot) provider.GetRequiredService<IConfiguration>();
            fileProvider ??= provider.GetRequiredService<IFileProvider>();
            var options = provider.GetRequiredService<IOptionsMonitor<T>>();
            return new WritableOptions<T>(fileProvider, options, configuration, section.Key, file);
        });
    }
}