using MementoMori.Api.Infrastructure;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Character;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 角色升阶处理器
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class CharacterRankUpHandler : IGameActionHandler
{
    private readonly ILogger<CharacterRankUpHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    public string ActionName => "角色升阶";

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, "正在检查角色升阶...");

        try
        {
            var autoJobSettings = await _playerSettingService.GetAutoJobSettingsAsync(userId);
            if (!autoJobSettings.AutoRankUpCharacter)
            {
                await _jobLogger.LogAsync(userId, "角色升阶未开启，跳过。");
                return;
            }

            var rankUpCount = 0;

            // R卡升阶 (需要3张)
            rankUpCount += await RankUp(nm, CharacterRarityFlags.R, 3);

            // SR卡升阶 (需要2张)
            rankUpCount += await RankUp(nm, CharacterRarityFlags.SR, 2);

            if (rankUpCount > 0)
            {
                await _jobLogger.LogAsync(userId, $"角色升阶完成，共升阶 {rankUpCount} 个角色。");
            }
            else
            {
                await _jobLogger.LogAsync(userId, "暂无可升阶的角色。");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "角色升阶失败 for user {UserId}", userId);
        }
    }

    private async Task<int> RankUp(NetworkManager nm, CharacterRarityFlags rarityFlags, int requiredCount)
    {
        var rankUpCount = 0;

        var characterGroups = nm.UserSyncData.UserCharacterDtoInfos
            .Where(d => (d.RarityFlags & rarityFlags) != 0)
            .GroupBy(d => d.CharacterId)
            .Where(g => g.Count() >= requiredCount)
            .ToList();

        foreach (var grouping in characterGroups)
        {
            var infos = grouping.OrderByDescending(d => d.Level).ToList();
            var main = infos.First();
            infos.RemoveAt(0);

            var materials = new List<MementoMori.Ortega.Share.Data.DtoInfo.UserCharacterDtoInfo>();
            var needCount = requiredCount - 1;

            while (needCount > 0 && infos.Count > 0)
            {
                var last = infos.Last();
                infos.Remove(last);
                materials.Add(last);
                needCount--;
            }

            if (materials.Count < requiredCount - 1) continue;

            try
            {
                await nm.SendRequest<RankUpRequest, RankUpResponse>(
                    new RankUpRequest
                    {
                        RankUpList = new List<CharacterRankUpMaterialInfo>
                        {
                            new()
                            {
                                TargetGuid = main.Guid,
                                MaterialGuid1 = materials[0].Guid,
                                MaterialGuid2 = materials.Count > 1 ? materials[1].Guid : null
                            }
                        }
                    }, false);
                rankUpCount++;
            }
            catch (Exception)
            {
                // 继续尝试其他角色
            }
        }

        return rankUpCount;
    }
}
