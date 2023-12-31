﻿using Microsoft.Extensions.Options;
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
    private AccountManager _accountManager;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public GuildRaidBossReleaseJob(IWritableOptions<GameConfig> gameConfig, AccountManager accountManager)
    {
        _accountManager = accountManager;
        _gameConfig = gameConfig;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.Value.AutoJob.AutoOpenGuildRaid)
        {
            return;
        }
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
        await account.Funcs.OpenGuildRaid();
    }
}