using MementoMori.Api.Services;
using MementoMori.Api.Extensions;
using MementoMori.Ortega.Share.Calculators;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Master.Data;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Enums;
using Newtonsoft.Json.Linq;
using static MementoMori.Api.Autopilot.AutopilotExtensions;

namespace MementoMori.Api.Autopilot;

/// <summary>
/// tamamo.dev 的全服通关队伍 (作者给的 API key, 放在配置 Tamamo:ApiKey, 不进 git).
/// 结果按关卡缓存, 不让每个客户端每次都去请求
/// </summary>
[RegisterSingleton]
[AutoConstructor]
public partial class TamamoService
{
    private static readonly HttpClient Http = new() {BaseAddress = new Uri("https://api.tamamo.dev/"), Timeout = TimeSpan.FromSeconds(30)};
    private static readonly TimeSpan IndexTtl = TimeSpan.FromHours(6);
    private static readonly TimeSpan PartyTtl = TimeSpan.FromHours(24);

    private readonly IConfiguration _config;
    private readonly TierListService _tiers;
    private readonly PlayerSettingService _settings;
    public const string SettingKey = "tamamo";

    private (DateTime at, JArray quests)? _index;
    private (DateTime at, JObject stages)? _usage;
    private readonly Dictionary<string, (DateTime at, List<ClearParty> list)> _parties = new();
    private readonly SemaphoreSlim _lock = new(1, 1);

    public class TamamoSettings
    {
        public string ApiKey { get; set; } = "";
    }

    /// <summary>用户自己在页面填的 key, 没有就用服务器配置的 (Tamamo:ApiKey / .env)</summary>
    public async Task<string?> GetKeyAsync(long userId)
    {
        var mine = (await _settings.GetSettingAsync<TamamoSettings>(userId, SettingKey))?.ApiKey;
        var key = string.IsNullOrWhiteSpace(mine) ? _config["Tamamo:ApiKey"] : mine.Trim();
        return string.IsNullOrWhiteSpace(key) ? null : key;
    }

    public Task SetKeyAsync(long userId, string? key) => _settings.SaveSettingAsync(userId, SettingKey, new TamamoSettings {ApiKey = key?.Trim() ?? ""});

    public record ClearParty(long DeckBattlePower, int Rank, List<ClearPartyCharacterInfo> ClearPartyCharacterInfos);
    public record TeamCharacter(long CharacterId, string Name, CharacterRarityFlags RarityFlags, string? Tier, bool Owned);
    public record TeamSuggestion(List<TeamCharacter> Characters, int Clears, long MinPower);
    public record UsageEntry(TeamCharacter Character, int Count);
    public record StageTeams(string Stage, string? DataStage, long Stuck, List<TeamSuggestion> Teams, TeamSuggestion? Lowest, string Link,
        string? UsageStage = null, List<UsageEntry>? Usage = null);

    /// <summary>当前关卡的通关队伍; 这关没数据就用后面最近的有数据的关卡</summary>
    public async Task<StageTeams?> GetQuestTeamsAsync(string key, long questId, string stageName, IEnumerable<UserCharacterDtoInfo> owned, bool refresh)
    {
        await _lock.WaitAsync();
        try
        {
            if (refresh || _index == null || DateTime.Now - _index.Value.at > IndexTtl)
            {
                var data = await GetAsync(key, "getClearParty");
                _index = (DateTime.Now, (JArray) data["ClearPartyInfoList"]![0]!["Quest"]!);
            }
            if (refresh || _usage == null || DateTime.Now - _usage.Value.at > IndexTtl)
            {
                // 一次拿全部主线关卡的角色使用次数 (type=0), 跟索引一样 6 小时一次
                try { _usage = (DateTime.Now, (JObject) (await GetAsync(key, "getClearPartyUseCharacter?type=0"))["Stages"]!); }
                catch (HttpRequestException) { }
            }

            var quests = _index.Value.quests;
            var here = quests.FirstOrDefault(q => (long) q["StageId"]! == questId);
            var source = quests.Where(q => (long) q["StageId"]! >= questId && !string.IsNullOrEmpty((string?) q["SearchId"]))
                .MinBy(q => (long) q["StageId"]!);
            var link = "https://tamamo.dev/ClearPartys";
            if (source == null) return new StageTeams(stageName, null, (long?) here?["StuckPlayer"] ?? 0, new(), null, link);

            var searchId = (string) source["SearchId"]!;
            if (refresh || !_parties.TryGetValue(searchId, out var cached) || DateTime.Now - cached.at > PartyTtl)
            {
                var list = (await GetAsync(key, $"getClearPartyList?id={Uri.EscapeDataString(searchId)}"))["result"]!.ToObject<List<ClearParty>>()!;
                cached = (DateTime.Now, list);
                _parties[searchId] = cached;
            }

            var ownedIds = owned.Select(c => c.CharacterId).ToHashSet();
            var ownedRarity = owned.GroupBy(c => c.CharacterId).ToDictionary(g => g.Key, g => g.Max(c => c.RarityFlags));
            TeamCharacter Char(long id) => new(id, CharName(id), ownedRarity.GetValueOrDefault(id), _tiers.GetTier(id), ownedIds.Contains(id));
            List<TeamCharacter> Chars(ClearParty p) => p.ClearPartyCharacterInfos
                .Select(c => new TeamCharacter(c.CharacterId, CharName(c.CharacterId), c.RarityFlags, _tiers.GetTier(c.CharacterId), ownedIds.Contains(c.CharacterId))).ToList();
            // 同一组角色 (不管站位) 算一种队伍, 按通关次数排
            var teams = cached.list.Where(p => p.ClearPartyCharacterInfos?.Count > 0)
                .GroupBy(p => string.Join(",", p.ClearPartyCharacterInfos.Select(c => c.CharacterId).OrderBy(i => i)))
                .OrderByDescending(g => g.Count()).ThenBy(g => g.Min(p => p.DeckBattlePower))
                .Take(3)
                .Select(g => new TeamSuggestion(Chars(g.First()), g.Count(), g.Min(p => p.DeckBattlePower)))
                .ToList();
            var lowest = cached.list.Where(p => p.ClearPartyCharacterInfos?.Count > 0).MinBy(p => p.DeckBattlePower);
            // 使用次数: 当前关卡或后面最近的有数据的关卡, 前 10
            var usageStage = _usage?.stages.Properties().Where(p => p.Value.HasValues && StageKey(p.Name) >= StageKey(stageName))
                .MinBy(p => StageKey(p.Name));
            var usage = usageStage?.Value.Children<JProperty>().OrderByDescending(p => (int) p.Value).Take(10)
                .Select(p => new UsageEntry(Char(long.Parse(p.Name)), (int) p.Value)).ToList();
            return new StageTeams(stageName, (string?) source["StageName"], (long?) here?["StuckPlayer"] ?? 0, teams,
                lowest == null ? null : new TeamSuggestion(Chars(lowest), 1, lowest.DeckBattlePower), link, usageStage?.Name, usage);
        }
        finally
        {
            _lock.Release();
        }
    }

    /// <summary>"6-5" → 6005, 排序用</summary>
    private static int StageKey(string stage)
    {
        var parts = stage.Split('-');
        return parts.Length == 2 && int.TryParse(parts[0], out var a) && int.TryParse(parts[1], out var b) ? a * 1000 + b : 0;
    }

    /// <summary>
    /// 把一次主线通关上传到 tamamo (作者的 postClearParty), 补上 tamamo 拿不到的关卡; 用户在 Autopilot 里打开才传.
    /// 失败只记日志, 不影响推图
    /// </summary>
    public async Task UploadQuestClearAsync(string? key, UserSyncData data, long worldId, long questId, BattleSimulationResult sim)
    {
        var st = data.UserStatusDtoInfo;
        var report = JObject.FromObject(sim);
        var attackers = sim.BattleField.Characters.Where(c => c.DefaultPosition.IsAttacker).ToList();
        var power = attackers.ToDictionary(c => c.CharacterGuid, c => BattlePowerCalculator.CalculateBattlePower(c.DefaultBattleParameter, c.DefaultBaseParameter));
        var party = attackers.Select(c => new JObject
        {
            ["CharacterGuid"] = c.CharacterGuid, ["CharacterId"] = c.UnitId, ["RarityFlags"] = (int) c.CharacterRarityFlags, ["Level"] = c.CharacterLevel,
            ["IsLevelLink"] = data.IsLevelLinkMember(c.CharacterGuid), ["Position"] = c.DefaultPosition.DeckIndex,
            ["BaseParameter"] = JObject.FromObject(c.DefaultBaseParameter), ["BattleParameter"] = JObject.FromObject(c.DefaultBattleParameter),
            ["BattlePower"] = power[c.CharacterGuid],
            // 装备里的 PlayerId/CreateAt 对通关没用, 不传
            ["UserEquipmentDtoInfos"] = new JArray(c.EquipmentDtoInfos.Select(e =>
            {
                var o = JObject.FromObject(e);
                o.Remove("PlayerId");
                o.Remove("CreateAt");
                return o;
            })),
        });
        var body = new JObject
        {
            ["Client"] = "MementoMori-Helper", ["PlayerId"] = st.PlayerId, ["PlayerName"] = st.Name, ["Rank"] = st.Rank,
            ["Type"] = 0, ["Stage"] = QuestTable.GetById(questId)?.Memo, ["StageId"] = questId, ["WorldId"] = worldId,
            ["BattleToken"] = sim.BattleToken, ["ClearedAt"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(), ["EndTurn"] = sim.BattleEndInfo.EndTurn,
            ["DeckBattlePower"] = power.Values.Sum(), ["ClearPartyCharacterInfos"] = new JArray(party),
            // 完整战斗记录 (BattleSimulationResult), tamamo 用它回放 "バトルレポート"
            ["BattleReport"] = report,
        };
        using var req = new HttpRequestMessage(HttpMethod.Post, "postClearParty") {Content = new StringContent(body.ToString(Newtonsoft.Json.Formatting.None), System.Text.Encoding.UTF8, "application/json")};
        // 作者说上传不需要 key; 有的话还是带上
        if (key != null) req.Headers.TryAddWithoutValidation("Authorization", $"ApiKey {key}");
        req.Headers.TryAddWithoutValidation("User-Agent", "MementoMori-Helper (+https://github.com/maplehugs/mementomori-helper)");
        using var resp = await Http.SendAsync(req);
        resp.EnsureSuccessStatusCode();
    }

    private async Task<JToken> GetAsync(string key, string path)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, path);
        req.Headers.TryAddWithoutValidation("Authorization", $"ApiKey {key}");
        // 没有 User-Agent 会 400; 顺便让作者知道是谁在请求
        req.Headers.TryAddWithoutValidation("User-Agent", "MementoMori-Helper (+https://github.com/maplehugs/mementomori-helper)");
        using var resp = await Http.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        return JObject.Parse(await resp.Content.ReadAsStringAsync())["data"]!;
    }
}
