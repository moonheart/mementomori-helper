using MementoMori.Ortega.Custom;
using MementoMori.Ortega.Common.Utils;
using MementoMori.Ortega.Share.Data.Item;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MementoMori.Funcs;

[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum GrowthKind
{
    Level, LevelLink, Equip, Reinforce, Quest, Tower, RankUp, Gacha, Team, Friend, Reward, Info
}

public record GrowthEvent(DateTimeOffset Time, GrowthKind Kind, string Text);

public record ProgressSnapshot(DateTimeOffset Time, long QuestId, string QuestName, long LinkLevel, long PlayerRank,
    long Gold, long Diamonds, Dictionary<string, long> Towers);

public class ProgressHistory
{
    public List<GrowthEvent> Events { get; set; } = new();
    public List<ProgressSnapshot> Snapshots { get; set; } = new();
}

// 每个账号一份进度历史, 存在 Data/progress-{playerId}.json, 用来在界面上证明 bot 真的在升级
public partial class MementoMoriFuncs
{
    private const int MaxEvents = 1000;
    private const int MaxSnapshots = 24 * 60; // 约60天的小时快照
    private ProgressHistory _history;
    private readonly object _historyLock = new();

    [Reactive]
    public string CurrentTask { get; private set; } = "";

    private string HistoryPath => Path.Combine("Data", $"progress-{UserSyncData?.UserStatusDtoInfo?.PlayerId ?? 0}.json");

    public ProgressHistory History
    {
        get
        {
            lock (_historyLock)
            {
                if (_history != null) return _history;
                try
                {
                    _history = File.Exists(HistoryPath) ? JsonConvert.DeserializeObject<ProgressHistory>(File.ReadAllText(HistoryPath)) : null;
                }
                catch (Exception e)
                {
                    AddLog($"progress history unreadable, starting fresh: {e.Message}");
                }

                return _history ??= new ProgressHistory();
            }
        }
    }

    public void Track(GrowthKind kind, string text)
    {
        lock (_historyLock)
        {
            History.Events.Add(new GrowthEvent(DateTimeOffset.Now, kind, text));
            if (History.Events.Count > MaxEvents) History.Events.RemoveRange(0, History.Events.Count - MaxEvents);
        }

        AddLog($"[{kind}] {text}");
        SaveHistory();
        this.RaisePropertyChanged(nameof(History));
    }

    // 领奖记一行: "登录奖励: 金币 ×1000, 钻石 ×20"
    public void TrackRewards(string source, params IEnumerable<IUserItem>?[] lists)
    {
        var items = lists.Where(l => l != null).SelectMany(l => l!).Where(i => i.ItemCount > 0)
            .GroupBy(i => (i.ItemType, i.ItemId)).Select(g => $"{ItemUtil.GetItemName(g.Key.ItemType, g.Key.ItemId)} ×{g.Sum(i => i.ItemCount):N0}").ToList();
        if (items.Count > 0) Track(GrowthKind.Reward, $"{source}: {string.Join(", ", items)}");
    }

    public void TakeSnapshot()
    {
        if (UserSyncData?.UserStatusDtoInfo == null) return;
        var questId = UserSyncData.UserBattleBossDtoInfo?.BossClearMaxQuestId ?? 0;
        var snap = new ProgressSnapshot(DateTimeOffset.Now, questId, QuestTable.GetById(questId)?.Memo ?? "-",
            UserSyncData.UserLevelLinkDtoInfo?.PartyLevel ?? 0, UserSyncData.UserStatusDtoInfo.Rank,
            UserSyncData.GetUserItemCount(ItemType.Gold), UserSyncData.GetUserItemCount(ItemType.CurrencyFree, isAnyCurrency: true),
            (UserSyncData.UserTowerBattleDtoInfos ?? new()).ToDictionary(t => t.TowerType.ToString(), t => t.MaxTowerBattleId));
        lock (_historyLock)
        {
            History.Snapshots.Add(snap);
            if (History.Snapshots.Count > MaxSnapshots) History.Snapshots.RemoveRange(0, History.Snapshots.Count - MaxSnapshots);
        }

        SaveHistory();
        this.RaisePropertyChanged(nameof(History));
    }

    private void SaveHistory()
    {
        try
        {
            string json;
            lock (_historyLock) json = JsonConvert.SerializeObject(_history);
            Directory.CreateDirectory("Data");
            var tmp = HistoryPath + ".tmp";
            File.WriteAllText(tmp, json);
            File.Move(tmp, HistoryPath, true);
        }
        catch (Exception e)
        {
            AddLog($"save progress history failed: {e.Message}");
        }
    }

    private async Task RunTask(string name, Func<Task> task)
    {
        CurrentTask = name;
        try
        {
            await task();
        }
        finally
        {
            CurrentTask = "";
        }
    }
}
