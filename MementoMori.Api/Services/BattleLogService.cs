using System.Text.Json;
using MementoMori.Api.Infrastructure.Database;
using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Api.Services;

/// <summary>
/// 战斗日志服务
/// </summary>
[RegisterSingleton]
public class BattleLogService
{
    private readonly IFreeSql _db;
    private readonly ILogger<BattleLogService> _logger;

    /// <summary>
    /// 需要拦截保存战斗日志的 API 列表
    /// </summary>
    private static readonly HashSet<string> BattleApis = new(StringComparer.OrdinalIgnoreCase)
    {
        "battle/boss",
        "battle/pvpStart",
        "battle/legendLeagueStart",
        "towerBattle/start",
        "dungeonBattle/execBattle",
        "guildRaid/startGuildRaid",
        "guildTower/battle"
    };

    public BattleLogService(IFreeSql db, ILogger<BattleLogService> logger)
    {
        _db = db;
        _logger = logger;
    }

    /// <summary>
    /// 尝试保存战斗日志 (失败不影响主流程)
    /// </summary>
    public async Task TrySaveBattleLogAsync(long userId, string apiUri, object response)
    {
        if (!BattleApis.Contains(apiUri))
        {
            return;
        }

        try
        {
            var entity = ExtractBattleData(userId, apiUri, response);
            if (entity == null)
            {
                return;
            }

            // 检查是否已存在 (防止重复保存)
            var exists = await _db.Select<BattleLogEntity>()
                .AnyAsync(b => b.BattleToken == entity.BattleToken);

            if (exists)
            {
                _logger.LogDebug("战斗日志已存在: {BattleToken}", entity.BattleToken);
                return;
            }

            await _db.Insert(entity).ExecuteAffrowsAsync();
            _logger.LogInformation("战斗日志已保存: {BattleToken}, 类型: {BattleType}, 胜利: {IsWin}",
                entity.BattleToken, entity.BattleType, entity.IsWin);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存战斗日志失败: {ApiUri}", apiUri);
            // 不影响主流程
        }
    }

    /// <summary>
    /// 从响应中提取战斗数据
    /// </summary>
    private BattleLogEntity? ExtractBattleData(long userId, string apiUri, object response)
    {
        try
        {
            BattleResult? battleResult = null;
            BattleSimulationResult? simulationResult = null;

            // 根据 API 路径提取战斗结果
            switch (apiUri.ToLowerInvariant())
            {
                case "battle/boss":
                    battleResult = GetPropertyValue<BattleResult>(response, "BattleResult");
                    break;

                case "battle/pvpstart":
                    battleResult = GetPropertyValue<BattleResult>(response, "BattleResult");
                    break;

                case "battle/legendleaguestart":
                    battleResult = GetPropertyValue<BattleResult>(response, "BattleResult");
                    break;

                case "towerbattle/start":
                    battleResult = GetPropertyValue<BattleResult>(response, "BattleResult");
                    break;

                case "dungeonbattle/execbattle":
                    simulationResult = GetPropertyValue<BattleSimulationResult>(response, "BattleSimulationResult");
                    break;

                case "guildraid/startguildraid":
                    battleResult = GetPropertyValue<BattleResult>(response, "BattleResult");
                    break;

                case "guildtower/battle":
                    battleResult = GetPropertyValue<BattleResult>(response, "BattleResult");
                    break;
            }

            // 统一处理
            if (battleResult != null)
            {
                simulationResult = battleResult.SimulationResult;
            }

            if (simulationResult == null)
            {
                _logger.LogWarning("无法从响应中提取战斗结果: {ApiUri}", apiUri);
                return null;
            }

            var endInfo = simulationResult.BattleEndInfo;
            var battleField = simulationResult.BattleField;
            var battleTime = battleResult?.BattleTime;

            return new BattleLogEntity
            {
                BattleToken = simulationResult.BattleToken,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                BattleType = (int)battleField.BattleType,
                QuestId = battleResult?.QuestId,
                IsWin = endInfo.WinGroupType == BattleFieldCharacterGroupType.Attacker,
                EndTurn = endInfo.EndTurn,
                BattleDurationMs = CalculateBattleDuration(battleTime),
                ApiUri = apiUri,
                BattleDataJson = JsonSerializer.Serialize(simulationResult, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = false
                })
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "提取战斗数据失败: {ApiUri}", apiUri);
            return null;
        }
    }

    /// <summary>
    /// 获取对象属性值
    /// </summary>
    private T? GetPropertyValue<T>(object obj, string propertyName) where T : class
    {
        try
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property == null)
            {
                // 尝试小写开头
                property = obj.GetType().GetProperty(char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1));
            }

            return property?.GetValue(obj) as T;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 计算战斗时长
    /// </summary>
    private long CalculateBattleDuration(BattleTime? battleTime)
    {
        if (battleTime == null)
        {
            return 0;
        }

        return (battleTime.EndBattle - battleTime.StartBattle) / 10000; // 转换为毫秒
    }

    /// <summary>
    /// 获取用户战斗日志列表
    /// </summary>
    public async Task<List<BattleLogEntity>> GetUserBattleLogsAsync(
        long userId,
        int? battleType = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        int page = 1,
        int pageSize = 20)
    {
        var query = _db.Select<BattleLogEntity>()
            .Where(b => b.UserId == userId);

        if (battleType.HasValue)
        {
            query = query.Where(b => b.BattleType == battleType.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(b => b.CreatedAt >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(b => b.CreatedAt <= endDate.Value);
        }

        return await query
            .OrderByDescending(b => b.CreatedAt)
            .Page(page, pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// 获取战斗日志总数
    /// </summary>
    public async Task<long> GetUserBattleLogCountAsync(
        long userId,
        int? battleType = null,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var query = _db.Select<BattleLogEntity>()
            .Where(b => b.UserId == userId);

        if (battleType.HasValue)
        {
            query = query.Where(b => b.BattleType == battleType.Value);
        }

        if (startDate.HasValue)
        {
            query = query.Where(b => b.CreatedAt >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(b => b.CreatedAt <= endDate.Value);
        }

        return await query.CountAsync();
    }

    /// <summary>
    /// 根据 Token 获取战斗日志详情
    /// </summary>
    public async Task<BattleLogEntity?> GetByTokenAsync(string battleToken, long userId)
    {
        return await _db.Select<BattleLogEntity>()
            .Where(b => b.BattleToken == battleToken && b.UserId == userId)
            .FirstAsync();
    }

    /// <summary>
    /// 删除战斗日志
    /// </summary>
    public async Task<bool> DeleteAsync(string battleToken, long userId)
    {
        var result = await _db.Delete<BattleLogEntity>()
            .Where(b => b.BattleToken == battleToken && b.UserId == userId)
            .ExecuteAffrowsAsync();

        return result > 0;
    }

    /// <summary>
    /// 批量删除旧日志 (后台清理用)
    /// </summary>
    public async Task<long> DeleteOldLogsAsync(DateTime beforeDate)
    {
        return await _db.Delete<BattleLogEntity>()
            .Where(b => b.CreatedAt < beforeDate)
            .ExecuteAffrowsAsync();
    }
}
