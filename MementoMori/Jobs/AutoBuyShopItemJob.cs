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
    private MementoMoriFuncs _mementoMoriFuncs;

    public AutoBuyShopItemJob(MementoMoriFuncs mementoMoriFuncs)
    {
        _mementoMoriFuncs = mementoMoriFuncs;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        if (!_mementoMoriFuncs.IsQuickActionExecuting) await _mementoMoriFuncs.Login();
        await _mementoMoriFuncs.AutoBuyShopItem();
    }
}