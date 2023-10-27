using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Option;

using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

internal class PvpJob : IJob
{
    private AccountManager _accountManager;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public PvpJob(IWritableOptions<GameConfig> gameConfig, AccountManager accountManager)
    {
        _gameConfig = gameConfig;
        _accountManager = accountManager;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.Value.AutoJob.AutoPvp) return;
        
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();

        await account.Funcs.PvpAuto();
        await account.Funcs.CompleteMissions();
        await account.Funcs.RewardMissonActivity();
    }
}