using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.Quest;
using static MementoMori.Ortega.Share.Masters;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 自动BOSS挑战处理器（自动推进主线BOSS关卡）
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class AutoBossChallengeHandler : IGameActionHandler
{
    private readonly ILogger<AutoBossChallengeHandler> _logger;
    private readonly JobLogger _jobLogger;
    private const int Max_Err_Count = 5;

    public string ActionName => "自动BOSS挑战";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "开始自动BOSS挑战...");

        var totalCount = 0;
        var winCount = 0;
        var errCount = 0;

        try
        {
            // 获取地图信息
            await nm.SendRequest<MapInfoRequest, MapInfoResponse>(
                new MapInfoRequest { IsUpdateOtherPlayerInfo = true }, 
                false);

            // 尝试获取下一个任务
            try
            {
                await nm.SendRequest<NextQuestRequest, NextQuestResponse>(new NextQuestRequest(), false);
            }
            catch (NetworkManager.ApiErrorException ex) when (ex.ErrorCode == ErrorCode.BattleAutoNextQuestNotFound)
            {
                // 忽略这个错误，继续执行
            }

            // 持续挑战直到失败或达到目标
            while (true)
            {
                try
                {
                    var targetQuestId = nm.UserSyncData.UserBattleBossDtoInfo.BossClearMaxQuestId + 1;
                    
                    // 获取任务信息
                    await nm.SendRequest<GetQuestInfoRequest, GetQuestInfoResponse>(
                        new GetQuestInfoRequest { TargetQuestId = targetQuestId }, 
                        false);
                    
                    // 执行BOSS战斗
                    var bossResponse = await nm.SendRequest<BossRequest, BossResponse>(
                        new BossRequest { QuestId = targetQuestId }, 
                        false);
                    
                    var win = bossResponse.BattleResult.SimulationResult.BattleEndInfo.IsWinAttacker();
                    totalCount++;
                    if (win) winCount++;

                    // 获取任务信息用于日志
                    var questInfo = QuestTable.GetById(targetQuestId);
                    var resultText = win ? "胜利" : "失败";
                    await _jobLogger.LogAsync(userId, 
                        $"BOSS关卡 {questInfo?.Memo ?? targetQuestId.ToString()}: {resultText} (总{totalCount}次, 胜{winCount}次, 错{errCount}次)");

                    if (win)
                    {
                        // 胜利后更新地图信息
                        await nm.SendRequest<MapInfoRequest, MapInfoResponse>(
                            new MapInfoRequest { IsUpdateOtherPlayerInfo = true }, 
                            false);
                        
                        // 尝试进入下一个任务
                        try
                        {
                            await nm.SendRequest<NextQuestRequest, NextQuestResponse>(new NextQuestRequest(), false);
                        }
                        catch (NetworkManager.ApiErrorException ex) when (ex.ErrorCode == ErrorCode.BattleAutoNextQuestNotFound)
                        {
                            await _jobLogger.LogAsync(userId, "已到达当前最大可挑战关卡");
                            break;
                        }
                    }
                    else
                    {
                        // 失败则停止
                        await _jobLogger.LogAsync(userId, "BOSS挑战失败，停止自动挑战");
                        break;
                    }

                    // 延迟一下避免请求过快
                    await Task.Delay(1000);
                }
                catch (Exception ex)
                {
                    errCount++;
                    await _jobLogger.LogAsync(userId, $"BOSS挑战出错: {ex.Message}", "Error");
                    
                    if (errCount > Max_Err_Count)
                    {
                        await _jobLogger.LogAsync(userId, $"错误次数超过{Max_Err_Count}次，停止自动挑战");
                        break;
                    }
                }
            }

            await _jobLogger.LogAsync(userId, $"自动BOSS挑战结束 - 总计{totalCount}次，胜利{winCount}次");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in AutoBossChallengeHandler for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"自动BOSS挑战失败: {ex.Message}", "Error");
            throw;
        }
    }
}
