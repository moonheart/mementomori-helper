using MementoMori.Api.Services;
using Microsoft.AspNetCore.Mvc;
using MementoMori.Api.Extensions;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Data.Item;
using Newtonsoft.Json;
using static MementoMori.Api.Autopilot.AutopilotExtensions;

namespace MementoMori.Api.Autopilot;

[ApiController]
[Route("api/autopilot")]
[AutoConstructor]
public partial class AutopilotController : ControllerBase
{
    private readonly PlayerSettingService _settings;
    private readonly JobManagerService _jobs;
    private readonly GrowthTracker _tracker;
    private readonly TierListService _tiers;
    private readonly BannerAdvisor _advisor;
    private readonly AccountManager _accounts;
    private readonly IServiceProvider _services;

    public record StepInfo(string Key, string Name);

    public record TierRow(long CharacterId, string Guid, CharacterRarityFlags RarityFlags, string Name, string? Tier, bool Overridden, string? Role, string? Notes, string Rarity, string Element, long Level, bool Linked, bool InTeam, bool Recommended);

    public record DrawRequest(long ButtonId, string Name);

    [HttpGet("steps")]
    public List<StepInfo> GetSteps() =>
        AutopilotJob.Steps.Select(t => new StepInfo(AutopilotJob.Key(t), ((Handlers.IGameActionHandler) _services.GetRequiredService(t)).ActionName)).ToList();

    [HttpGet("{userId}/config")]
    public async Task<AutopilotConfig> GetConfig(long userId) =>
        await _settings.GetSettingAsync<AutopilotConfig>(userId, AutopilotJob.SettingKey) ?? new();

    [HttpPost("{userId}/config")]
    public async Task<ActionResult> SaveConfig(long userId, [FromBody] AutopilotConfig config)
    {
        await _settings.SaveSettingAsync(userId, AutopilotJob.SettingKey, config);
        await _jobs.RegisterJobsAsync(userId); // cron/开关变了要重新注册
        return Ok();
    }

    /// <summary>立即跑一轮 (或只跑某一步)</summary>
    [HttpPost("{userId}/run")]
    public async Task<ActionResult> Run(long userId, [FromQuery] string? step = null)
    {
        await _jobs.RunOnceAsync<AutopilotJob>(userId, step == null ? null : new Dictionary<string, string> {["step"] = step});
        return Ok();
    }

    [HttpGet("{userId}/events")]
    public Task<List<GrowthEventEntity>> GetEvents(long userId, [FromQuery] int limit = 200) => _tracker.GetEventsAsync(userId, Math.Clamp(limit, 1, 1000));

    [HttpGet("{userId}/snapshots")]
    public async Task<object> GetSnapshots(long userId, [FromQuery] int days = 30)
    {
        var list = await _tracker.GetSnapshotsAsync(userId, DateTime.Now.AddDays(-Math.Clamp(days, 1, 90)));
        return list.Select(s => new
        {
            s.Time, s.QuestId, s.QuestName, s.LinkLevel, s.PlayerRank, s.Gold, s.Diamonds, s.Power,
            Towers = JsonConvert.DeserializeObject<Dictionary<string, long>>(s.TowersJson)
        });
    }

    /// <summary>拥有的角色 + 强度, 用于 Team & Tiers 页面</summary>
    public record Stats(long QuestId, string QuestName, long LinkLevel, long Tower, long Rank, long Power, long Gold, long Diamonds);

    public record TeamMember(long CharacterId, string Guid, CharacterRarityFlags RarityFlags, string Name, string? Tier, string Rarity, long Level, int Gear, int AvgReinforce);

    public record Overview(bool Enabled, DateTime? NextRun, Stats Now, Stats? DayAgo, List<TeamMember> Team);

    /// <summary>Home 页面: autopilot 状态 + 当前数值 + 24 小时前的数值 + 主队</summary>
    [HttpGet("{userId}/overview")]
    public async Task<ActionResult<Overview>> GetOverview(long userId)
    {
        var data = (await _accounts.GetOrCreateAsync(userId)).NetworkManager.UserSyncData;
        if (data?.UserStatusDtoInfo == null) return NotFound("not logged in");
        var config = await GetConfig(userId);
        var nextRun = (await _jobs.GetJobStatusAsync(userId)).FirstOrDefault(j => j.JobType == nameof(AutopilotJob))?.NextRunTime;

        var questId = data.UserBattleBossDtoInfo?.BossClearMaxQuestId ?? 0;
        var now = new Stats(questId, QuestTable.GetById(questId)?.Memo ?? "-", data.UserLevelLinkDtoInfo?.PartyLevel ?? 0,
            data.UserTowerBattleDtoInfos?.FirstOrDefault(t => t.TowerType == TowerType.Infinite)?.MaxTowerBattleId ?? 0,
            data.UserStatusDtoInfo.Rank, GrowthTracker.TeamPower(data),
            data.GetUserItemCount(ItemType.Gold), data.GetUserItemCount(ItemType.CurrencyFree, isAnyCurrency: true));

        var snaps = await _tracker.GetSnapshotsAsync(userId, DateTime.Now.AddDays(-3));
        var old = snaps.LastOrDefault(s => s.Time <= DateTime.Now.AddHours(-24)) ?? snaps.FirstOrDefault();
        Stats? dayAgo = old == null ? null : new Stats(old.QuestId, old.QuestName, old.LinkLevel,
            JsonConvert.DeserializeObject<Dictionary<string, long>>(old.TowersJson)?.GetValueOrDefault(nameof(TowerType.Infinite)) ?? 0,
            old.PlayerRank, old.Power, old.Gold, old.Diamonds);

        var team = data.GetMainTeam().Select(c =>
        {
            var gear = data.UserEquipmentDtoInfos.Where(e => e.CharacterGuid == c.Guid).ToList();
            return new TeamMember(c.CharacterId, c.Guid, c.RarityFlags, CharName(c.CharacterId), _tiers.GetTier(c.CharacterId), c.RarityFlags.ToString(),
                data.GetEffectiveLevel(c), gear.Count, gear.Count == 0 ? 0 : (int) gear.Average(e => e.ReinforcementLv));
        }).ToList();

        return new Overview(config.Enabled, nextRun, now, dayAgo, team);
    }

    [HttpGet("{userId}/tiers")]
    public async Task<List<TierRow>> GetTiers(long userId)
    {
        var data = (await _accounts.GetOrCreateAsync(userId)).NetworkManager.UserSyncData;
        if (data?.UserCharacterDtoInfos == null) return new();
        var team = data.GetMainTeam().Select(c => c.Guid).ToHashSet();
        var recommended = _tiers.GetRecommendedTeam(data).Select(c => c.Guid).ToHashSet();
        return data.UserCharacterDtoInfos
            .GroupBy(c => c.CharacterId).Select(g => g.MaxBy(c => c.RarityFlags)!)
            .Select(c =>
            {
                var entry = _tiers.GetBuiltinEntry(c.CharacterId);
                var mb = CharacterTable.GetById(c.CharacterId);
                return new TierRow(c.CharacterId, c.Guid, c.RarityFlags, CharName(c.CharacterId), _tiers.GetTier(c.CharacterId), _tiers.IsOverridden(c.CharacterId),
                    entry?.Role, entry?.Notes, c.RarityFlags.ToString(), mb?.ElementType.ToString() ?? "", data.GetEffectiveLevel(c), data.IsLevelLinkMember(c.Guid),
                    team.Contains(c.Guid), recommended.Contains(c.Guid));
            })
            .OrderByDescending(r => _tiers.Score(r.CharacterId)).ThenByDescending(r => r.Level)
            .ToList();
    }

    [HttpPut("tiers/{characterId}")]
    public ActionResult SetTier(long characterId, [FromBody] string? tier)
    {
        if (tier != null && tier != "" && !TierListService.Tiers.Contains(tier)) return BadRequest("tier must be SS/S/A/B/C or empty");
        _tiers.SetTier(characterId, tier);
        return Ok();
    }

    [HttpGet("{userId}/banners")]
    public async Task<List<BannerAdvice>> GetBanners(long userId)
    {
        var ctx = await _accounts.GetOrCreateAsync(userId);
        return await _advisor.EvaluateAsync(ctx.NetworkManager, await GetConfig(userId));
    }

    [HttpPost("{userId}/banners/draw")]
    public async Task<ActionResult> Draw(long userId, [FromBody] DrawRequest request)
    {
        var ctx = await _accounts.GetOrCreateAsync(userId);
        await _advisor.DrawAsync(userId, ctx.NetworkManager, request.ButtonId, request.Name);
        return Ok();
    }
}
