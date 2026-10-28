using MementoMori.Ortega.Share.Data.Battle;
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

    private (DateTime at, JArray quests)? _index;
    private readonly Dictionary<string, (DateTime at, List<ClearParty> list)> _parties = new();
    private readonly SemaphoreSlim _lock = new(1, 1);

    public bool Enabled => !string.IsNullOrWhiteSpace(ApiKey);
    private string? ApiKey => _config["Tamamo:ApiKey"];

    public record ClearParty(long DeckBattlePower, int Rank, List<ClearPartyCharacterInfo> ClearPartyCharacterInfos);
    public record TeamCharacter(long CharacterId, string Name, CharacterRarityFlags RarityFlags, string? Tier, bool Owned);
    public record TeamSuggestion(List<TeamCharacter> Characters, int Clears, long MinPower);
    public record StageTeams(string Stage, string? DataStage, long Stuck, List<TeamSuggestion> Teams, TeamSuggestion? Lowest, string Link);

    /// <summary>当前关卡的通关队伍; 这关没数据就用后面最近的有数据的关卡</summary>
    public async Task<StageTeams?> GetQuestTeamsAsync(long questId, string stageName, IEnumerable<UserCharacterDtoInfo> owned, bool refresh)
    {
        if (!Enabled) return null;
        await _lock.WaitAsync();
        try
        {
            if (refresh || _index == null || DateTime.Now - _index.Value.at > IndexTtl)
            {
                var data = await GetAsync("getClearParty");
                _index = (DateTime.Now, (JArray) data["ClearPartyInfoList"]![0]!["Quest"]!);
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
                var list = (await GetAsync($"getClearPartyList?id={Uri.EscapeDataString(searchId)}"))["result"]!.ToObject<List<ClearParty>>()!;
                cached = (DateTime.Now, list);
                _parties[searchId] = cached;
            }

            var ownedIds = owned.Select(c => c.CharacterId).ToHashSet();
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
            return new StageTeams(stageName, (string?) source["StageName"], (long?) here?["StuckPlayer"] ?? 0, teams,
                lowest == null ? null : new TeamSuggestion(Chars(lowest), 1, lowest.DeckBattlePower), link);
        }
        finally
        {
            _lock.Release();
        }
    }

    private async Task<JToken> GetAsync(string path)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, path);
        req.Headers.TryAddWithoutValidation("Authorization", $"ApiKey {ApiKey}");
        using var resp = await Http.SendAsync(req);
        resp.EnsureSuccessStatusCode();
        return JObject.Parse(await resp.Content.ReadAsStringAsync())["data"]!;
    }
}
