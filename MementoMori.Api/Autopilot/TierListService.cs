using System.Reflection;
using MementoMori.Ortega.Share.Data;
using MementoMori.Ortega.Share.Data.DtoInfo;
using Newtonsoft.Json;

namespace MementoMori.Api.Autopilot;

public record TierEntry(string Name, string Tier, string Role, string Element, string Notes);

/// <summary>
/// 角色强度表: 内置社区榜单 (按名字匹配) + 用户覆盖 (SQLite, CharacterId -> tier)
/// </summary>
[RegisterSingleton]
public class TierListService
{
    public static readonly string[] Tiers = {"SS", "S", "A", "B", "C"};

    private readonly IFreeSql _db;
    private readonly List<TierEntry> _builtin;
    private Dictionary<long, string>? _overrides;
    private readonly object _lock = new();

    public TierListService(IFreeSql db)
    {
        _db = db;
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MementoMori.Api.tierlist.default.json");
        _builtin = stream == null ? new() : JsonConvert.DeserializeObject<List<TierEntry>>(new StreamReader(stream).ReadToEnd()) ?? new();
    }

    private Dictionary<long, string> Overrides
    {
        get
        {
            lock (_lock) return _overrides ??= _db.Select<TierOverrideEntity>().ToList().ToDictionary(o => o.CharacterId, o => o.Tier);
        }
    }

    public TierEntry? GetBuiltinEntry(long characterId)
    {
        var mb = CharacterTable.GetById(characterId);
        if (mb == null) return null;
        var name = Normalize(TextResourceTable.Get(mb.NameKey));
        var full = Normalize(mb.GetCombinedName());
        // 名字可能带称号 ("Natasha, The Witch of ..."), 先全名再短名
        return _builtin.FirstOrDefault(t => Normalize(t.Name) == full) ?? _builtin.FirstOrDefault(t => Normalize(t.Name) == name);
    }

    private static string Normalize(string? s) => new string((s ?? "").Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();

    public string? GetTier(long characterId) =>
        Overrides.TryGetValue(characterId, out var t) ? t : GetBuiltinEntry(characterId)?.Tier;

    public bool IsOverridden(long characterId) => Overrides.ContainsKey(characterId);

    public void SetTier(long characterId, string? tier)
    {
        lock (_lock)
        {
            if (string.IsNullOrEmpty(tier) || tier == GetBuiltinEntry(characterId)?.Tier)
            {
                Overrides.Remove(characterId);
                _db.Delete<TierOverrideEntity>().Where(o => o.CharacterId == characterId).ExecuteAffrows();
            }
            else
            {
                Overrides[characterId] = tier;
                _db.InsertOrUpdate<TierOverrideEntity>().SetSource(new TierOverrideEntity {CharacterId = characterId, Tier = tier}).ExecuteAffrows();
            }
        }
    }

    /// <summary>SS=5 ... C=1, 没有=0</summary>
    public int Score(long characterId) => GetTier(characterId) switch
    {
        "SS" => 5, "S" => 4, "A" => 3, "B" => 2, "C" => 1, _ => 0
    };

    /// <summary>
    /// 推荐队伍: 每个角色取稀有度最高的一张, 按 强度 → 稀有度 → 等级 排, 有像样的奶就保证带一个
    /// </summary>
    public List<UserCharacterDtoInfo> GetRecommendedTeam(UserSyncData data)
    {
        if (data?.UserCharacterDtoInfos == null) return new();
        var pool = data.UserCharacterDtoInfos
            .GroupBy(c => c.CharacterId).Select(g => g.MaxBy(c => c.RarityFlags)!)
            .OrderByDescending(c => Score(c.CharacterId)).ThenByDescending(c => c.RarityFlags).ThenByDescending(c => data.GetEffectiveLevel(c))
            .ToList();
        var team = pool.Take(5).ToList();
        bool IsHealer(UserCharacterDtoInfo c) => GetBuiltinEntry(c.CharacterId)?.Role == "healer";
        if (team.Count == 5 && !team.Any(IsHealer))
        {
            var healer = pool.Skip(5).FirstOrDefault(c => IsHealer(c) && Score(c.CharacterId) >= 2);
            if (healer != null) team[4] = healer;
        }

        return team;
    }
}
