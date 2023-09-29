using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

[DisallowConcurrentExecution]
[Cron("0 29 20 ? * *")]
internal class PvpJob : IJob
{
    private MementoMoriFuncs _mementoMoriFuncs;
    private readonly GameConfig _gameConfig;

    public PvpJob(MementoMoriFuncs mementoMoriFuncs, IOptions<GameConfig> gameConfig)
    {
        _mementoMoriFuncs = mementoMoriFuncs;
        _gameConfig = gameConfig.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.AutoJob.AutoPvp) return;

        if (_mementoMoriFuncs.IsQuickActionExecuting)
        {
            _mementoMoriFuncs.CancelQuickAction();
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
        else
        {
            await _mementoMoriFuncs.Login();
        }

        await _mementoMoriFuncs.PvpAuto();
        await _mementoMoriFuncs.CompleteMissions();
        await _mementoMoriFuncs.RewardMissonActivity();
    }
}