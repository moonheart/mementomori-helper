using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoMori.Option;
using Quartz;

namespace MementoMori.Jobs;

[DisallowConcurrentExecution]
internal class AutoBuyShopItemJob : IJob
{
    private AccountManager _accountManager;

    public AutoBuyShopItemJob(AccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        foreach (var (_, account) in _accountManager.GetAll())
        {
            if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
            await account.Funcs.AutoBuyShopItem();
        }
    }
}