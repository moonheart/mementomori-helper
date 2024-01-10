using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoCtor;
using MementoMori.Option;
using Quartz;

namespace MementoMori.Jobs;

[AutoConstruct]
internal partial class AutoBuyShopItemJob : IJob
{
    private readonly AccountManager _accountManager;

    public async Task Execute(IJobExecutionContext context)
    {
        var userId = context.MergedJobDataMap.GetLongValue("userId");
        if (userId <= 0) return;
        var account = _accountManager.Get(userId);
        if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
        await account.Funcs.AutoBuyShopItem();
    }
}