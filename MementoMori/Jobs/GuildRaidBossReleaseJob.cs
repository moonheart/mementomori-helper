using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoMori.Jobs;

internal class GuildRaidBossReleaseJob : IJob
{
    private MementoMoriFuncs _mementoMoriFuncs;
    private readonly GameConfig _gameConfig;

    public GuildRaidBossReleaseJob(MementoMoriFuncs mementoMoriFuncs, IOptions<GameConfig> gameConfig)
    {
        _mementoMoriFuncs = mementoMoriFuncs;
        _gameConfig = gameConfig.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (_mementoMoriFuncs.IsQuickActionExecuting) return;

        await _mementoMoriFuncs.Login();
        await _mementoMoriFuncs.OpenGuildRaid();
    }
}