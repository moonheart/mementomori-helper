using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Option;

using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

[DisallowConcurrentExecution]
internal class PvpJob : IJob
{
    private MementoMoriFuncs _mementoMoriFuncs;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public PvpJob(MementoMoriFuncs mementoMoriFuncs, IWritableOptions<GameConfig> gameConfig)
    {
        _mementoMoriFuncs = mementoMoriFuncs;
        _gameConfig = gameConfig;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.Value.AutoJob.AutoPvp) return;

        if (!_mementoMoriFuncs.IsQuickActionExecuting) await _mementoMoriFuncs.Login();

        await _mementoMoriFuncs.PvpAuto();
        await _mementoMoriFuncs.CompleteMissions();
        await _mementoMoriFuncs.RewardMissonActivity();
    }
}