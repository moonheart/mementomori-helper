using AutoCtor;
using EleCho.GoCqHttpSdk;
using EleCho.GoCqHttpSdk.MessageMatching;
using MementoMori.BotServer.Options;
using MementoMori.BotServer.Plugins;
using MementoMori.Option;

namespace MementoMori.BotServer;

[InjectSingleton]
[AutoConstruct]
public partial class SessionAccessor
{
    private readonly IWritableOptions<BotOptions> _botOptions;
    private static CqWsSession? _session;

    private static readonly object _lock = new();

    public Action<CqWsSession>? OnConnected;

    public CqWsSession Session
    {
        get
        {
            if (_session is {IsConnected: true}) return _session;
            lock (_lock)
            {
                if (_session is {IsConnected: true})
                {
                    return _session;
                }

                _session = new CqWsSession(new CqWsSessionOptions()
                {
                    BaseUri = new Uri(_botOptions.Value.BaseUri)
                });

                OnConnected?.Invoke(_session);

                _ = _session.StartAsync();

                while (!_session.IsConnected)
                {
                    Thread.Sleep(100);
                }
            }

            return _session;
        }
    }
}