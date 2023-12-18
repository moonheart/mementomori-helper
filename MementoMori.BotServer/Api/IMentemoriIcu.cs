using Refit;

namespace MementoMori.BotServer.Api;

public interface IMentemoriIcu
{
    [Get("/worlds")]
    Task<Result<List<WorldInfo>>> Worlds();

    [Get("/wgroups")]
    Task<Result<List<WorldGroup>>> Wgroups();

    [Get("/{world_id}/player_ranking/latest")]
    Task<Result<PlayerRanking>> PlayerRanking(int world_id);

    [Get("/{world_id}/guild_ranking/latest")]
    Task<Result<GuildRankingInfo>> GuildRanking(int world_id);

    [Get("/{world_id}/temple/latest")]
    Task<Result<TempleInfo>> Temple(int world_id);

    [Get("/{world_id}/arena/latest")]
    Task<Result<List<ArenaInfo>>> Arena(int world_id);
}

public class Result<T>
{
    public int status { get; set; }
    public long timestamp { get; set; }
    public T data { get; set; }
}

public class WorldInfo
{
    public int world_id { get; set; }
    public bool ranking { get; set; }
    public bool temple { get; set; }
    public bool localgvg { get; set; }
}

public class WorldGroup
{
    public int group_id { get; set; }
    public int[] worlds { get; set; }
    public bool legendleague { get; set; }
    public bool globalgvg { get; set; }
}

public class PlayerInfo
{
    public long id { get; set; }
    public string name { get; set; }
    public long bp { get; set; }
    public int rank { get; set; }
    public int quest_id { get; set; }
    public int tower_id { get; set; }
    public long icon_id { get; set; }
    public long guild_id { get; set; }
    public string guild_name { get; set; }
    public long guild_join_time { get; set; }
    public int guild_position { get; set; }
    public int prev_legend_league_class { get; set; }
}

public class PlayerRankingInfo
{
    public List<PlayerInfo> bp { get; set; }
    public List<PlayerInfo> rank { get; set; }
    public List<PlayerInfo> quest { get; set; }
    public List<PlayerInfo> tower { get; set; }
}

public class PlayerRanking
{
    public int world_id { get; set; }
    public PlayerRankingInfo rankings { get; set; }
    public Dictionary<string, PlayerInfo> player_info { get; set; }
}

public class GuildInfo
{
    public long id { get; set; }
    public string name { get; set; }
    public string bp { get; set; }
    public int level { get; set; }
    public int stock { get; set; }
    public int exp { get; set; }
    public int num_members { get; set; }
    public long leader_id { get; set; }
    public string leader_name { get; set; }
    public int policy { get; set; }
    public string description { get; set; }
    public bool free_join { get; set; }
    public int bp_requirement { get; set; }
}

public class GuildRankingInfo
{
    public List<GuildInfo> bp { get; set; }
    public List<GuildInfo> level { get; set; }
    public List<GuildInfo> stock { get; set; }
}

public class TempleInfo
{
    public int world_id { get; set; }
    public int[] quest_ids { get; set; }
}

public class ArenaInfo
{
    public string PlayerName { get; set; }
    public int PlayerLevel { get; set; }
    public int LatestQuestId { get; set; }
    public int LatestTowerBattleQuestId { get; set; }
    public UserCharacterInfoList[] UserCharacterInfoList { get; set; }
}

public class UserCharacterInfoList
{
    public int CharacterId { get; set; }
    public int Level { get; set; }
    public int SubLevel { get; set; }
    public int Exp { get; set; }
    public int RarityFlags { get; set; }
    public UserEquipmentDtoInfos[] UserEquipmentDtoInfos { get; set; }
    public BaseParameter BaseParameter { get; set; }
    public BattleParameter BattleParameter { get; set; }
    public int BattlePower { get; set; }
}

public class UserEquipmentDtoInfos
{
    public int AdditionalParameterEnergy { get; set; }
    public int AdditionalParameterHealth { get; set; }
    public int AdditionalParameterIntelligence { get; set; }
    public int AdditionalParameterMuscle { get; set; }
    public int EquipmentId { get; set; }
    public string Guid { get; set; }
    public int LegendSacredTreasureExp { get; set; }
    public int LegendSacredTreasureLv { get; set; }
    public int MatchlessSacredTreasureExp { get; set; }
    public int MatchlessSacredTreasureLv { get; set; }
    public int ReinforcementLv { get; set; }
    public int SphereId1 { get; set; }
    public int SphereId2 { get; set; }
    public int SphereId3 { get; set; }
    public int SphereId4 { get; set; }
    public int SphereUnlockedCount { get; set; }
    public string CharacterGuid { get; set; }
    public long CreateAt { get; set; }
    public long PlayerId { get; set; }
}

public class BaseParameter
{
    public int Energy { get; set; }
    public int Health { get; set; }
    public int Intelligence { get; set; }
    public int Muscle { get; set; }
}

public class BattleParameter
{
    public int AttackPower { get; set; }
    public int Avoidance { get; set; }
    public int Critical { get; set; }
    public int CriticalDamageEnhance { get; set; }
    public int CriticalResist { get; set; }
    public int DamageEnhance { get; set; }
    public int DamageReflect { get; set; }
    public int DebuffHit { get; set; }
    public int DebuffResist { get; set; }
    public int Defense { get; set; }
    public int DefensePenetration { get; set; }
    public int HP { get; set; }
    public int Hit { get; set; }
    public int HpDrain { get; set; }
    public int MagicCriticalDamageRelax { get; set; }
    public int MagicDamageRelax { get; set; }
    public int PhysicalCriticalDamageRelax { get; set; }
    public int PhysicalDamageRelax { get; set; }
    public int Speed { get; set; }
}