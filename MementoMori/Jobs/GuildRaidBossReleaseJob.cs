using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Option;
using AutoCtor;

namespace MementoMori.Jobs;

[AutoConstruct]
internal partial class GuildRaidBossReleaseJob : IJob
{
    private readonly IWritableOptions<GameConfig> _gameConfig;
    private readonly AccountManager _accountManager;

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_gameConfig.Value.AutoJob.AutoOpenGuildRaid) return;
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
        await account.Funcs.OpenGuildRaid();
    }
}