using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Option;

namespace MementoMori.Jobs;

internal class GuildRaidBossReleaseJob : IJob
{
    private MementoMoriFuncs _mementoMoriFuncs;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public GuildRaidBossReleaseJob(MementoMoriFuncs mementoMoriFuncs, IWritableOptions<GameConfig> gameConfig)
    {
        _mementoMoriFuncs = mementoMoriFuncs;
        _gameConfig = gameConfig;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.Value.AutoJob.AutoOpenGuildRaid)
        {
            return;
        }
        if (!_mementoMoriFuncs.IsQuickActionExecuting) await _mementoMoriFuncs.Login();
        await _mementoMoriFuncs.OpenGuildRaid();
    }
}