using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MementoMori;

/// <summary>
/// 为每个用户创建一个 IoC 容器, 用于管理依赖
/// </summary>
public class DependencyManager
{
    private readonly ConcurrentDictionary<string, IServiceProvider> _dependencyContainers = new();

    public void SetBuildAction(string account, Action<IServiceCollection> buildAction)
    {
        var serviceCollection = new ServiceCollection();
        buildAction(serviceCollection);
        serviceCollection.MakeReadOnly();
        _dependencyContainers[account] = serviceCollection.BuildServiceProvider();
    }

    public IServiceProvider GetContainer(string account)
    {
        return _dependencyContainers.TryGetValue(account, out var container) ? container : default;
    }

    public T GetService<T>(string account)
    {
        var container = GetContainer(account);
        return container.GetService(typeof(T)) is T service ? service : default;
    }
}