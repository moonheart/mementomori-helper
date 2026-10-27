using MementoMori.Api.Extensions;
using MementoMori.Api.Infrastructure;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.Item;
using Newtonsoft.Json;

namespace MementoMori.Api.Autopilot;

/// <summary>
/// 成长记录 + 进度快照, 存在 SQLite
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class GrowthTracker
{
    private const int KeepEvents = 1000;
    private const int KeepSnapshots = 24 * 60; // 约60天的小时快照

    private readonly IFreeSql _db;
    private readonly JobLogger _jobLogger;

    public async Task TrackAsync(long userId, GrowthKind kind, string text)
    {
        await _db.Insert(new GrowthEventEntity {UserId = userId, Time = DateTime.Now, Kind = kind, Text = text}).ExecuteAffrowsAsync();
        await _jobLogger.LogAsync(userId, $"[{kind}] {text}");
        await TrimAsync<GrowthEventEntity>(userId, KeepEvents);
    }

    public async Task SnapshotAsync(long userId, UserSyncData data)
    {
        if (data?.UserStatusDtoInfo == null) return;
        var questId = data.UserBattleBossDtoInfo?.BossClearMaxQuestId ?? 0;
        await _db.Insert(new ProgressSnapshotEntity
        {
            UserId = userId,
            Time = DateTime.Now,
            QuestId = questId,
            QuestName = QuestTable.GetById(questId)?.Memo ?? "-",
            LinkLevel = data.UserLevelLinkDtoInfo?.PartyLevel ?? 0,
            PlayerRank = data.UserStatusDtoInfo.Rank,
            Gold = data.GetUserItemCount(ItemType.Gold),
            Diamonds = data.GetUserItemCount(ItemType.CurrencyFree, isAnyCurrency: true),
            TowersJson = JsonConvert.SerializeObject((data.UserTowerBattleDtoInfos ?? new()).ToDictionary(t => t.TowerType.ToString(), t => t.MaxTowerBattleId))
        }).ExecuteAffrowsAsync();
        await TrimAsync<ProgressSnapshotEntity>(userId, KeepSnapshots);
    }

    public Task<List<GrowthEventEntity>> GetEventsAsync(long userId, int limit) =>
        _db.Select<GrowthEventEntity>().Where(e => e.UserId == userId).OrderByDescending(e => e.Time).Take(limit).ToListAsync();

    public Task<List<ProgressSnapshotEntity>> GetSnapshotsAsync(long userId, DateTime since) =>
        _db.Select<ProgressSnapshotEntity>().Where(s => s.UserId == userId && s.Time >= since).OrderBy(s => s.Time).ToListAsync();

    private async Task TrimAsync<T>(long userId, int keep) where T : class
    {
        // ponytail: 每次写入都数一遍, 量很小无所谓
        var select = _db.Select<T>().Where($"UserId = {userId}");
        var count = await select.CountAsync();
        if (count <= keep) return;
        var oldIds = await _db.Select<T>().Where($"UserId = {userId}").OrderBy("Time").Take((int) (count - keep)).ToListAsync<long>("Id");
        await _db.Delete<T>().Where($"Id in ({string.Join(",", oldIds)})").ExecuteAffrowsAsync();
    }
}
