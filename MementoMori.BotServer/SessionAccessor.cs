using EleCho.GoCqHttpSdk;
using MementoMori.BotServer.Options;
using MementoMori.Option;

namespace MementoMori.BotServer;

[InjectSingleton]
public class SessionAccessor
{
    private readonly IWritableOptions<BotOptions> _botOptions;
    private static CqWsSession? _session;

    private static readonly object _lock = new();

    public SessionAccessor(IWritableOptions<BotOptions> botOptions)
    {
        _botOptions = botOptions;
    }

    public CqWsSession Session
    {
        get
        {
            if (_session != null) return _session;
            lock (_lock)
            {
                _session ??= new CqWsSession(new CqWsSessionOptions()
                {
                    BaseUri = new Uri(_botOptions.Value.BaseUri)
                });
            }

            return _session;
        }
    }
}