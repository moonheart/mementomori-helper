using MementoMori.Option;
using Microsoft.Extensions.Options;
using Quartz;

namespace MementoMori.Jobs;

public class HourlyJob : IJob
{
    private AccountManager _accountManager;
    private readonly IWritableOptions<GameConfig> _gameConfig;

    public HourlyJob(IWritableOptions<GameConfig> gameConfig, AccountManager accountManager)
    {
        _gameConfig = gameConfig;
        _accountManager = accountManager;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        foreach (var (_, account) in _accountManager.GetAll())
        {
            
            if (!account.Funcs.IsQuickActionExecuting) await account.Funcs.Login();
            await account.Funcs.GetLoginBonus();
            await account.Funcs.BountyQuestStartAuto();
            await account.Funcs.PresentReceiveItem();
            await account.Funcs.GetAutoBattleReward();
            await account.Funcs.GuildRaid();
            await account.Funcs.ReceiveGvgReward();
            await account.Funcs.BulkTransferFriendPoint();
            await account.Funcs.BountyQuestRewardAuto();
            await account.Funcs.CompleteMissions();
            await account.Funcs.RewardMissonActivity();
            if (_gameConfig.Value.AutoJob.AutoFreeGacha) await account.Funcs.FreeGacha();
            if (_gameConfig.Value.AutoJob.AutoUseItems) await account.Funcs.AutoUseItems();
        }
    }
}