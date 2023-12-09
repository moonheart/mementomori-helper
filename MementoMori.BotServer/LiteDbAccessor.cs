using LiteDB;
using MementoMori.BotServer.Options;
using Microsoft.Extensions.Options;

namespace MementoMori.BotServer;

[InjectSingleton]
public class LiteDbAccessor
{
    private readonly IOptions<BotOptions> _botOptions;
    public LiteDatabase GetDb() => new(_botOptions.Value.LiteDbConnStr);

    public LiteDbAccessor(IOptions<BotOptions> botOptions)
    {
        _botOptions = botOptions;
    }
}