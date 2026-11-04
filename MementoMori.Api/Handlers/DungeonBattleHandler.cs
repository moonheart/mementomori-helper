using MementoMori.Api.Infrastructure;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Api.Utils;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.ApiInterface.Battle;
using MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle;
using MementoMori.Ortega.Share.Data.ApiInterface.Equipment;
using MementoMori.Ortega.Share.Data.DungeonBattle;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Api.Handlers;

/// <summary>
/// 时空洞窟 (Cave of Space-Time). Port of the old MementoMori/Funcs/DungeonBattle.cs:
/// best path per layer (event items > shops with wanted items > treasure chests (PreferTreasureChest) > battles > recovery > revival),
/// every grid type (guests, boss without relic, special event battles, battle + relic reinforce), shop purchases from
/// ShopTargetItems, unequip during events (AutoRemoveEquipment) and hard mode on layer 3 when unlocked.
/// </summary>
[RegisterTransient]
[AutoConstructor]
public partial class DungeonBattleHandler : IGameActionHandler
{
    private readonly ILogger<DungeonBattleHandler> _logger;
    private readonly JobLogger _jobLogger;
    private readonly PlayerSettingService _playerSettingService;

    // 11 rows per layer (1,2,3,2,3,2,3,2,3,2,1 grids); a full run is a few dozen steps, this only stops a stuck loop.
    private const int MaxSteps = 300;

    public string ActionName => Text.T("ActionDungeonBattle");

    public async Task ExecuteAsync(AccountContext context)
    {
        var userId = context.AccountInfo.UserId;
        var nm = context.NetworkManager;

        await _jobLogger.LogAsync(userId, Text.T("DungeonChecking"));

        try
        {
            var settings = await _playerSettingService.GetDungeonBattleSettingsAsync(userId);
            var autoJobSettings = await _playerSettingService.GetAutoJobSettingsAsync(userId);
            if (!autoJobSettings.AutoDungeonBattle)
            {
                await _jobLogger.LogAsync(userId, Text.T("DungeonAutoDisabled"));
                return;
            }

            await UnequipForEventAsync(nm, userId, settings);

            var info = await GetInfo(nm);
            if (info.UserDungeonDtoInfo.IsDoneRewardClearLayer(3))
            {
                await _jobLogger.LogAsync(userId, Text.T("DungeonAlreadyCleared"));
                return;
            }

            await _jobLogger.LogAsync(userId, Text.T("DungeonStarting", info.CurrentDungeonBattleLayer?.LayerCount ?? 0));

            var bestPaths = new Dictionary<int, List<DungeonBattleGrid>>();
            var gridData = new Dictionary<string, GetBattleGridDataResponse>();

            for (var step = 0; step < MaxSteps; step++)
            {
                info = await GetInfo(nm);
                if (info.UserDungeonDtoInfo.IsDoneRewardClearLayer(3))
                {
                    await _jobLogger.LogAsync(userId, Text.T("DungeonCompleted"));
                    return;
                }

                var layer = info.CurrentDungeonBattleLayer;
                if (layer == null) break;
                var gridMbs = layer.DungeonGrids.ToDictionary(g => g.DungeonGridGuid, g => Masters.DungeonBattleGridTable.GetById(g.DungeonGridId));

                // Rewards of battle grids (event items) for path scoring; fetched once per grid.
                foreach (var g in layer.DungeonGrids)
                {
                    if (gridData.ContainsKey(g.DungeonGridGuid) || !gridMbs[g.DungeonGridGuid].IsBattleType()) continue;
                    gridData[g.DungeonGridGuid] = await nm.SendRequest<GetBattleGridDataRequest, GetBattleGridDataResponse>(
                        new GetBattleGridDataRequest { CurrentTermId = info.CurrentTermId, DungeonGridGuid = g.DungeonGridGuid });
                }

                var current = layer.DungeonGrids.FirstOrDefault(g => g.DungeonGridGuid == info.UserDungeonDtoInfo.CurrentGridGuid);
                if (current == null) break;
                var type = gridMbs[current.DungeonGridGuid].DungeonGridType;

                switch (info.UserDungeonDtoInfo.CurrentGridState)
                {
                    case DungeonBattleGridState.Done:
                    {
                        var next = NextOnBestPath(info, layer, current, gridMbs, gridData, bestPaths, settings);
                        if (next == null)
                        {
                            if (!info.UserDungeonDtoInfo.IsDoneRewardClearLayer(layer.LayerCount))
                            {
                                await nm.SendRequest<RewardClearLayerRequest, RewardClearLayerResponse>(new RewardClearLayerRequest
                                {
                                    ClearedLayer = layer.LayerCount,
                                    CurrentTermId = info.CurrentTermId,
                                    DungeonBattleDifficultyType = layer.DungeonDifficultyType
                                });
                            }

                            if (layer.LayerCount >= 3)
                            {
                                await _jobLogger.LogAsync(userId, Text.T("DungeonCompleted"));
                                return;
                            }

                            var difficulty = layer.LayerCount == 2 && IsHardModeAvailable(nm) ? DungeonBattleDifficultyType.Hard : DungeonBattleDifficultyType.Normal;
                            await nm.SendRequest<ProceedLayerRequest, ProceedLayerResponse>(new ProceedLayerRequest
                            {
                                CurrentTermId = info.CurrentTermId,
                                DungeonDifficultyType = difficulty
                            });
                        }
                        else if (gridMbs[next.DungeonGridGuid].DungeonGridType == DungeonBattleGridType.JoinCharacter)
                        {
                            await JoinGuestAsync(nm, info, next, userId);
                        }
                        else
                        {
                            await nm.SendRequest<SelectGridRequest, SelectGridResponse>(new SelectGridRequest
                            {
                                CurrentTermId = info.CurrentTermId,
                                DungeonGridGuid = next.DungeonGridGuid
                            });
                        }

                        break;
                    }
                    case DungeonBattleGridState.Selected:
                        if (!await ExecuteGridAsync(nm, info, current, type, userId, settings)) return;
                        break;
                    case DungeonBattleGridState.Reward:
                        if (type == DungeonBattleGridType.BattleAndRelicReinforce)
                            await ReinforceRelicAsync(nm, info, current, userId);
                        else
                            await nm.SendRequest<RewardBattleReceiveRelicRequest, RewardBattleReceiveRelicResponse>(new RewardBattleReceiveRelicRequest
                            {
                                CurrentTermId = info.CurrentTermId,
                                DungeonGridGuid = current.DungeonGridGuid,
                                SelectedRelicId = info.RewardRelicIds?.FirstOrDefault() ?? 0
                            });
                        break;
                    default:
                        _logger.LogWarning("Cave: unexpected grid state {State} for user {UserId}", info.UserDungeonDtoInfo.CurrentGridState, userId);
                        return;
                }
            }

            await _jobLogger.LogAsync(userId, Text.T("DungeonDone"));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "地下城战斗失败 for user {UserId}", userId);
            await _jobLogger.LogAsync(userId, $"{Text.T("ActionDungeonBattle")}: {ex.Message}", "Error");
        }
    }

    private static Task<GetDungeonBattleInfoResponse> GetInfo(NetworkManager nm) =>
        nm.SendRequest<GetDungeonBattleInfoRequest, GetDungeonBattleInfoResponse>(new GetDungeonBattleInfoRequest());

    private static bool IsHardModeAvailable(NetworkManager nm) =>
        nm.UserSyncData.UserBattleBossDtoInfo?.BossClearMaxQuestId >=
        Masters.OpenContentTable.GetByOpenCommandType(OpenCommandType.DungeonBattleHardMode).OpenContentValue;

    /// <summary>Event Cave: characters enter with their stats as they are, so take gear off, enter, and put it back.</summary>
    private async Task UnequipForEventAsync(NetworkManager nm, long userId, GameConfig.DungeonBattleConfig settings)
    {
        if (!settings.AutoRemoveEquipment) return;
        var competition = await nm.SendRequest<GetCompetitionInfoRequest, GetCompetitionInfoResponse>(new GetCompetitionInfoRequest());
        if (!competition.IsDungeonBattleEventOpen) return;

        var equips = nm.UserSyncData.UserEquipmentDtoInfos.Where(d => !string.IsNullOrEmpty(d.CharacterGuid)).GroupBy(d => d.CharacterGuid).ToList();
        if (equips.Count == 0) return;
        await _jobLogger.LogAsync(userId, Text.T("DungeonUnequipping", equips.Count));
        foreach (var g in equips)
        {
            await nm.SendRequest<RemoveEquipmentRequest, RemoveEquipmentResponse>(new RemoveEquipmentRequest
            {
                UserCharacterGuid = g.Key,
                EquipmentSlotTypes = g.Select(d => Masters.EquipmentTable.GetById(d.EquipmentId).SlotType).ToList()
            });
        }

        await GetInfo(nm); // entering locks in the unequipped stats

        foreach (var g in equips)
        {
            await nm.SendRequest<ChangeEquipmentRequest, ChangeEquipmentResponse>(new ChangeEquipmentRequest
            {
                UserCharacterGuid = g.Key,
                EquipmentChangeInfos = g.Select(d => new EquipmentChangeInfo
                {
                    EquipmentGuid = d.Guid,
                    EquipmentId = d.EquipmentId,
                    EquipmentSlotType = Masters.EquipmentTable.GetById(d.EquipmentId).SlotType,
                    IsInherit = false
                }).ToList()
            });
        }
        await _jobLogger.LogAsync(userId, Text.T("DungeonReequipped", equips.Count));
    }

    /// <summary>Returns false when the run can't continue (everyone down, no recovery items left).</summary>
    private async Task<bool> ExecuteGridAsync(NetworkManager nm, GetDungeonBattleInfoResponse info, DungeonBattleGrid current,
        DungeonBattleGridType type, long userId, GameConfig.DungeonBattleConfig settings)
    {
        switch (type)
        {
            case DungeonBattleGridType.Start:
            case DungeonBattleGridType.JoinCharacter:
                return true;
            case DungeonBattleGridType.BattleNormal:
            case DungeonBattleGridType.BattleElite:
            case DungeonBattleGridType.BattleBoss:
            case DungeonBattleGridType.BattleBossNoRelic:
            case DungeonBattleGridType.EventBattleNormal:
            case DungeonBattleGridType.EventBattleElite:
            case DungeonBattleGridType.EventBattleSpecial:
            case DungeonBattleGridType.TreasureChest:
                return await DoBattleAsync(nm, info, current, userId, settings);
            case DungeonBattleGridType.BattleAndRelicReinforce:
                if (!await DoBattleAsync(nm, info, current, userId, settings)) return false;
                await ReinforceRelicAsync(nm, await GetInfo(nm), current, userId);
                return true;
            case DungeonBattleGridType.Recovery:
                await nm.SendRequest<ExecRecoveryRequest, ExecRecoveryResponse>(new ExecRecoveryRequest
                    { CurrentTermId = info.CurrentTermId, DungeonGridGuid = current.DungeonGridGuid, IsHealed = true });
                return true;
            case DungeonBattleGridType.Revival:
                await nm.SendRequest<ExecReviveRequest, ExecReviveResponse>(new ExecReviveRequest
                    { CurrentTermId = info.CurrentTermId, DungeonGridGuid = current.DungeonGridGuid, IsRevived = true });
                return true;
            case DungeonBattleGridType.RelicReinforce:
                await nm.SendRequest<ExecReinforceRelicRequest, ExecReinforceRelicResponse>(new ExecReinforceRelicRequest
                    { CurrentTermId = info.CurrentTermId, DungeonGridGuid = current.DungeonGridGuid });
                return true;
            case DungeonBattleGridType.Shop:
                await ShopAsync(nm, info, current, userId, settings);
                return true;
            default:
                _logger.LogWarning("Cave: unhandled grid type {Type} for user {UserId}", type, userId);
                return false;
        }
    }

    private async Task<bool> DoBattleAsync(NetworkManager nm, GetDungeonBattleInfoResponse info, DungeonBattleGrid current,
        long userId, GameConfig.DungeonBattleConfig settings)
    {
        // The Cave deck first, then the strongest living characters to fill 5 slots.
        var guids = new List<string>();
        var deck = nm.UserSyncData.UserDeckDtoInfos.FirstOrDefault(d => d.DeckUseContentType == DeckUseContentType.DungeonBattle);
        foreach (var guid in deck?.GetUserCharacterGuids() ?? new List<string>())
        {
            var c = info.UserDungeonBattleCharacterDtoInfos.FirstOrDefault(d => d.Guid == guid);
            if (c != null && c.CurrentHpPerMill > 0) guids.Add(guid);
        }

        guids.AddRange(info.UserDungeonBattleCharacterDtoInfos
            .Where(d => !guids.Contains(d.Guid) && d.CurrentHpPerMill > 0)
            .OrderByDescending(d => BattlePowerCalculatorUtil.GetUserCharacterBattlePower(nm, d.Guid))
            .Select(d => d.Guid)
            .Take(5 - guids.Count));

        if (guids.Count == 0)
        {
            if (info.UserDungeonDtoInfo.UseDungeonRecoveryItemCount < settings.MaxUseRecoveryItem)
            {
                await nm.SendRequest<UseRecoveryItemRequest, UseRecoveryItemResponse>(new UseRecoveryItemRequest { CurrentTermId = info.CurrentTermId });
                return true;
            }

            await _jobLogger.LogAsync(userId, Text.T("DungeonAllDown"));
            return false;
        }

        await nm.SendRequest<ExecBattleRequest, ExecBattleResponse>(new ExecBattleRequest
            { CurrentTermId = info.CurrentTermId, DungeonGridGuid = current.DungeonGridGuid, CharacterGuids = guids });
        await nm.SendRequest<FinishBattleRequest, FinishBattleResponse>(new FinishBattleRequest
            { DungeonGridGuid = current.DungeonGridGuid, CurrentTermId = info.CurrentTermId, VisitDungeonCount = 0 });
        return true;
    }

    private async Task ShopAsync(NetworkManager nm, GetDungeonBattleInfoResponse info, DungeonBattleGrid current, long userId,
        GameConfig.DungeonBattleConfig settings)
    {
        var shop = info.UserDungeonBattleShopDtoInfos?.FirstOrDefault(d => d.GridGuid == current.DungeonGridGuid);
        foreach (var item in shop?.TradeShopItemList ?? new())
        {
            var wanted = settings.ShopTargetItems.Any(t => t.ItemType == item.GiveItem.ItemType && t.ItemId == item.GiveItem.ItemId && item.SalePercent >= t.MinDiscountPercent);
            if (!wanted) continue;
            try
            {
                await nm.SendRequest<ExecShopRequest, ExecShopResponse>(new ExecShopRequest
                    { CurrentTermId = info.CurrentTermId, DungeonGridGuid = current.DungeonGridGuid, TradeShopItemId = item.TradeShopItemId });
                await _jobLogger.LogAsync(userId, Text.T("DungeonBought", ItemUtil.GetItemName(item.GiveItem.ItemType, item.GiveItem.ItemId), item.GiveItem.ItemCount));
            }
            catch (NetworkManager.ApiErrorException e)
            {
                // Usually not enough currency; keep going with the rest of the run.
                await _jobLogger.LogAsync(userId, $"{ItemUtil.GetItemName(item.GiveItem.ItemType, item.GiveItem.ItemId)}: {Masters.TextResourceTable.GetErrorCodeMessage(e.ErrorCode)}");
            }
        }

        await nm.SendRequest<LeaveShopRequest, LeaveShopResponse>(new LeaveShopRequest
            { CurrentTermId = info.CurrentTermId, DungeonGridGuid = current.DungeonGridGuid });
    }

    private async Task JoinGuestAsync(NetworkManager nm, GetDungeonBattleInfoResponse info, DungeonBattleGrid next, long userId)
    {
        var offered = info.UserDungeonDtoInfo.GuestCharacterMap.TryGetValue(next.DungeonGridGuid, out var ids)
            ? ids.Select(id => Masters.DungeonBattleGuestTable.GetById(id).CharacterId).ToHashSet()
            : new HashSet<long>();
        var guests = info.UserDungeonBattleGuestCharacterDtoInfos
            .Where(d => offered.Contains(d.CharacterId))
            .OrderByDescending(d => d.BattlePower);
        foreach (var guest in guests)
        {
            try
            {
                await nm.SendRequest<ExecGuestRequest, ExecGuestResponse>(new ExecGuestRequest
                    { DungeonGridGuid = next.DungeonGridGuid, GuestMBId = guest.CharacterId, CurrentTermId = info.CurrentTermId });
                await _jobLogger.LogAsync(userId, Text.T("DungeonGuestJoined",
                    Masters.TextResourceTable.Get(Masters.CharacterTable.GetById(guest.CharacterId).NameKey)));
                return;
            }
            catch (NetworkManager.ApiErrorException e)
            {
                _logger.LogInformation("Cave: guest {CharacterId} refused ({Error})", guest.CharacterId, e.ErrorCode);
            }
        }
    }

    /// <summary>Upgrade a relic you hold whose upgraded form you don't have yet (first one that works).</summary>
    private async Task ReinforceRelicAsync(NetworkManager nm, GetDungeonBattleInfoResponse info, DungeonBattleGrid current, long userId)
    {
        var held = info.UserDungeonDtoInfo.RelicIds ?? new List<long>();
        foreach (var relicId in held)
        {
            var upgraded = Masters.DungeonBattleRelicTable.GetByReinforceFrom(relicId);
            if (upgraded == null || held.Contains(upgraded.Id)) continue;
            try
            {
                await nm.SendRequest<RewardBattleReinforceRelicRequest, RewardBattleReinforceRelicResponse>(new RewardBattleReinforceRelicRequest
                    { CurrentTermId = info.CurrentTermId, DungeonGridGuid = current.DungeonGridGuid, SelectedRelicId = relicId });
                return;
            }
            catch (NetworkManager.ApiErrorException e) when (e.ErrorCode == ErrorCode.DungeonBattleAlreadyHaveRelic)
            {
            }
        }
    }

    // ── path ─────────────────────────────────────────────────────────────────

    private static DungeonBattleGrid? NextOnBestPath(GetDungeonBattleInfoResponse info, DungeonBattleLayer layer, DungeonBattleGrid current,
        Dictionary<string, DungeonBattleGridMB> gridMbs, Dictionary<string, GetBattleGridDataResponse> gridData,
        Dictionary<int, List<DungeonBattleGrid>> bestPaths, GameConfig.DungeonBattleConfig settings)
    {
        var rows = layer.DungeonGrids.GroupBy(d => d.Y).OrderBy(d => d.Key).Select(d => d.OrderBy(x => x.X).ToArray()).ToArray();

        if (!bestPaths.TryGetValue(layer.LayerCount, out var best) || best.All(d => d.DungeonGridGuid != current.DungeonGridGuid))
        {
            var paths = new List<List<DungeonBattleGrid>>();
            Walk(current, new List<DungeonBattleGrid>());
            if (paths.Count == 0) return null;
            best = paths.OrderByDescending(p => Score(p)).First();
            bestPaths[layer.LayerCount] = best;

            void Walk(DungeonBattleGrid grid, List<DungeonBattleGrid> path)
            {
                path.Add(grid);
                if (grid.Y >= rows.Length - 1)
                    paths.Add(new List<DungeonBattleGrid>(path));
                else
                    foreach (var n in rows[grid.Y + 1].Where(n => IsAdjacent(grid, n))) Walk(n, path);
                path.RemoveAt(path.Count - 1);
            }
        }

        var index = best.FindIndex(d => d.DungeonGridGuid == current.DungeonGridGuid);
        return index < best.Count - 1 ? best[index + 1] : null;

        bool IsAdjacent(DungeonBattleGrid from, DungeonBattleGrid to) =>
            rows[from.Y].Length > rows[to.Y].Length
                ? to.X == from.X || to.X == from.X - 1
                : to.X == from.X || to.X == from.X + 1;

        DungeonBattleGridType TypeOf(DungeonBattleGrid g) => gridMbs[g.DungeonGridGuid].DungeonGridType;

        // Compared in order, like the old version: event items, wanted shops, chests, battles, recovery, revival.
        (long, int, int, int, int, int) Score(List<DungeonBattleGrid> path) => (
            path.Sum(EventItems),
            path.Count(WantedShop),
            settings.PreferTreasureChest ? path.Count(g => TypeOf(g) == DungeonBattleGridType.TreasureChest) : 0,
            path.Count(g => gridMbs[g.DungeonGridGuid].IsBattleType()),
            path.Count(g => TypeOf(g) == DungeonBattleGridType.Recovery),
            path.Count(g => TypeOf(g) == DungeonBattleGridType.Revival));

        long EventItems(DungeonBattleGrid g)
        {
            var t = TypeOf(g);
            if (t != DungeonBattleGridType.EventBattleNormal && t != DungeonBattleGridType.EventBattleElite && t != DungeonBattleGridType.EventBattleSpecial) return 0;
            return gridData.TryGetValue(g.DungeonGridGuid, out var data)
                ? data.NormalRewardItemList?.FirstOrDefault(x => x.ItemType == info.EventItemType && x.ItemId == info.EventItemId)?.ItemCount ?? 0
                : 0;
        }

        bool WantedShop(DungeonBattleGrid g)
        {
            if (TypeOf(g) != DungeonBattleGridType.Shop || settings.ShopTargetItems.Count == 0) return false;
            var shop = info.UserDungeonBattleShopDtoInfos?.FirstOrDefault(s => s.GridGuid == g.DungeonGridGuid);
            return shop?.TradeShopItemList?.Any(i => settings.ShopTargetItems.Any(t =>
                i.SalePercent >= t.MinDiscountPercent && i.GiveItem.ItemType == t.ItemType && i.GiveItem.ItemId == t.ItemId)) == true;
        }
    }
}
