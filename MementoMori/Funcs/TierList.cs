using System.Reflection;
using Newtonsoft.Json;

namespace MementoMori.Funcs;

public record TierEntry(long Id, string Name, string Tier, string Role, string Element, string Notes);

// 角色强度表: 内置社区榜单 (tierlist.default.json, 按 CharacterId, 整理自 Game8 2026-10) + 用户在界面里改的覆盖 (Data/tierlist.json, CharacterId -> tier)
public partial class MementoMoriFuncs
{
    public static readonly string[] Tiers = {"SS", "S", "A", "B", "C"};
    private const string TierOverridePath = "Data/tierlist.json";
    private static Dictionary<long, TierEntry>? _builtinTiers;
    private static Dictionary<long, string>? _tierOverrides;
    private static readonly object TierLock = new();

    public static Dictionary<long, TierEntry> BuiltinTiers
    {
        get
        {
            if (_builtinTiers != null) return _builtinTiers;
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MementoMori.tierlist.default.json");
            var list = stream == null ? null : JsonConvert.DeserializeObject<List<TierEntry>>(new StreamReader(stream).ReadToEnd());
            return _builtinTiers = (list ?? new()).ToDictionary(t => t.Id);
        }
    }

    private static Dictionary<long, string> TierOverrides
    {
        get
        {
            lock (TierLock)
            {
                if (_tierOverrides != null) return _tierOverrides;
                try
                {
                    _tierOverrides = File.Exists(TierOverridePath) ? JsonConvert.DeserializeObject<Dictionary<long, string>>(File.ReadAllText(TierOverridePath)) : null;
                }
                catch
                {
                    _tierOverrides = null;
                }

                return _tierOverrides ??= new();
            }
        }
    }

    // 按 CharacterId 匹配, 跟界面语言无关
    public static TierEntry? GetBuiltinTierEntry(long characterId) => BuiltinTiers.GetValueOrDefault(characterId);

    public string? GetTier(long characterId) =>
        TierOverrides.TryGetValue(characterId, out var t) ? t : GetBuiltinTierEntry(characterId)?.Tier;

    public bool IsTierOverridden(long characterId) => TierOverrides.ContainsKey(characterId);

    public void SetTier(long characterId, string? tier)
    {
        lock (TierLock)
        {
            if (string.IsNullOrEmpty(tier) || tier == GetBuiltinTierEntry(characterId)?.Tier) TierOverrides.Remove(characterId);
            else TierOverrides[characterId] = tier;
            Directory.CreateDirectory("Data");
            File.WriteAllText(TierOverridePath, JsonConvert.SerializeObject(TierOverrides, Formatting.Indented));
        }
    }

    // SS=5 ... C=1, 没有=0; 用于排序和抽卡判断
    public int TierScore(long characterId) => GetTier(characterId) switch
    {
        "SS" => 5, "S" => 4, "A" => 3, "B" => 2, "C" => 1, _ => 0
    };
}

public partial class MementoMoriFuncs
{
    // 推荐队伍: 每个角色取稀有度最高的一张, 按 强度 → 稀有度 → 等级 排, 有像样的奶就保证带一个
    public List<Ortega.Share.Data.DtoInfo.UserCharacterDtoInfo> GetRecommendedTeam()
    {
        if (UserSyncData?.UserCharacterDtoInfos == null) return new();
        var pool = UserSyncData.UserCharacterDtoInfos
            .GroupBy(c => c.CharacterId).Select(g => g.MaxBy(c => c.RarityFlags)!)
            .OrderByDescending(c => TierScore(c.CharacterId)).ThenByDescending(c => c.RarityFlags).ThenByDescending(c => GetEffectiveLevel(c))
            .ToList();
        var team = pool.Take(5).ToList();
        bool IsHealer(Ortega.Share.Data.DtoInfo.UserCharacterDtoInfo c) => GetBuiltinTierEntry(c.CharacterId)?.Role == "healer";
        if (team.Count == 5 && !team.Any(IsHealer))
        {
            var healer = pool.Skip(5).FirstOrDefault(c => IsHealer(c) && TierScore(c.CharacterId) >= 2);
            if (healer != null) team[4] = healer;
        }

        return team;
    }
}
