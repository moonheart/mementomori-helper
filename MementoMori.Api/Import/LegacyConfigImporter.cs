using System.Text;
using MementoMori.Api.Infrastructure;
using MementoMori.Api.Infrastructure.Database;
using MementoMori.Api.Models;
using MementoMori.Api.Services;
using MementoMori.Ortega.Share.Data.ApiInterface.Auth;
using MementoMori.Ortega.Share.Data.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace MementoMori.Api.Import;

/// <summary>
/// 旧 Blazor 版配置的一次性导入器。
///
/// 只在新数据库没有账号时由 Program.cs 自动调用; 也可以通过
/// --import-legacy &lt;path&gt; [--dry-run] [--force] 手动执行。
/// 导入完成后运行时只使用新键名, 不再读旧 appsettings.user.json。
/// </summary>
[RegisterScoped]
[AutoConstructor]
public partial class LegacyConfigImporter
{
    private const string EquipmentReinforcementAction = "EquipmentReinforcement";

    private readonly IFreeSql _fsql;
    private readonly PlayerSettingService _settings;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;
    private readonly ILogger<LegacyConfigImporter> _logger;

    /// <summary>
    /// 自动检测旧配置: 环境变量/配置优先, 然后是工作目录、ContentRoot、程序目录。
    /// 只找 appsettings.user.json, 不递归扫描其它目录。
    /// </summary>
    public string? ResolveAutoPath()
    {
        var paths = ResolveAutoPaths().ToList();
        foreach (var path in paths)
        {
            try
            {
                if (HasAccounts(Load(path).AuthOption)) return path;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "读取候选旧配置 {Path} 失败", path);
            }
        }

        return paths.FirstOrDefault();
    }

    private IEnumerable<string> ResolveAutoPaths()
    {
        var configured = _configuration["LegacyImport:Path"];
        if (string.IsNullOrWhiteSpace(configured))
        {
            configured = Environment.GetEnvironmentVariable("LEGACY_IMPORT_PATH");
        }

        if (!string.IsNullOrWhiteSpace(configured))
        {
            var fullPath = Path.GetFullPath(configured);
            if (File.Exists(fullPath))
            {
                yield return fullPath;
            }
            else
            {
                _logger.LogWarning("LegacyImport:Path {Path} 不存在", fullPath);
            }
        }

        var candidates = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), "appsettings.user.json"),
            Path.Combine(_environment.ContentRootPath, "appsettings.user.json"),
            Path.Combine(AppContext.BaseDirectory, "appsettings.user.json"),
            // 源码目录/解压后旁边还留着旧 Blazor 项目时也能找到
            Path.Combine(Directory.GetCurrentDirectory(), "MementoMori.WebUI", "appsettings.user.json"),
            Path.Combine(_environment.ContentRootPath, "..", "MementoMori.WebUI", "appsettings.user.json"),
            Path.Combine(AppContext.BaseDirectory, "..", "MementoMori.WebUI", "appsettings.user.json")
        };

        foreach (var path in candidates
                     .Select(Path.GetFullPath)
                     .Distinct(StringComparer.OrdinalIgnoreCase))
        {
            if (File.Exists(path)) yield return path;
        }
    }

    /// <summary>
    /// 首次启动自动检测: 数据库没有账号、且找到了旧配置时才导入。
    /// 失败只记录日志, 不阻止程序启动。
    /// </summary>
    public async Task AutoImportAsync()
    {
        if (!_configuration.GetValue("LegacyImport:Enabled", true))
        {
            _logger.LogInformation("旧配置自动导入已禁用 (LegacyImport:Enabled=false)");
            return;
        }

        var accountCount = await _fsql.Select<AccountEntity>().CountAsync();
        if (accountCount > 0)
        {
            _logger.LogInformation("数据库已有 {Count} 个账号, 跳过旧配置自动导入", accountCount);
            return;
        }

        var paths = ResolveAutoPaths().ToList();
        if (paths.Count == 0)
        {
            _logger.LogInformation("没有找到 appsettings.user.json, 跳过旧配置自动导入");
            return;
        }

        _logger.LogInformation("检测到旧配置候选: {Paths}", string.Join(", ", paths));

        foreach (var path in paths)
        {
            try
            {
                var legacy = Load(path);
                if (!HasAccounts(legacy.AuthOption))
                {
                    _logger.LogInformation("找到旧配置 {Path}, 但没有可用账号, 继续查找", path);
                    continue;
                }

                var result = await ImportCoreAsync(path, legacy, force: false, dryRun: false);
                _logger.LogInformation("旧配置导入完成: {Summary}", result.ToSummary());
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "自动导入旧配置 {Path} 失败, 已跳过", path);
            }
        }

        _logger.LogInformation("所有候选旧配置都没有可用账号, 跳过导入");
    }

    /// <summary>
    /// 手动导入。path 指向旧 appsettings.user.json。
    /// </summary>
    public async Task<LegacyImportResult> ImportAsync(string path, bool force = false, bool dryRun = false)
    {
        var fullPath = Path.GetFullPath(path);
        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException("找不到旧配置文件", fullPath);
        }

        if (!dryRun && !force && await _fsql.Select<AccountEntity>().AnyAsync())
        {
            throw new InvalidOperationException("数据库里已有账号, 已拒绝重复导入; 如需覆盖请加 --force");
        }

        var legacy = Load(fullPath);
        return await ImportCoreAsync(fullPath, legacy, force, dryRun);
    }

    private async Task<LegacyImportResult> ImportCoreAsync(string path, LegacyRoot legacy, bool force, bool dryRun)
    {
        var result = new LegacyImportResult {SourcePath = path, DryRun = dryRun};
        var accounts = BuildAccounts(legacy, result);
        result.AccountsFound = accounts.Count;

        if (accounts.Count == 0)
        {
            result.Warnings.Add("旧配置里没有可用账号");
            return result;
        }

        if (!dryRun && !force && await _fsql.Select<AccountEntity>().AnyAsync())
        {
            throw new InvalidOperationException("数据库里已有账号, 已拒绝重复导入; 如需覆盖请加 --force");
        }

        _logger.LogInformation("开始导入旧配置 {Path}: {Count} 个账号{DryRun}",
            path, accounts.Count, dryRun ? " (dry-run)" : "");

        ReportLegacyOnlyFields(legacy, result);

        var resolutions = new Dictionary<long, PlayerResolution>();
        foreach (var account in accounts)
        {
            resolutions[account.UserId] = await ResolvePlayerAsync(account, legacy, result);
        }

        ReportUnusedPlayerOptions(legacy, resolutions.Values, result);

        if (dryRun)
        {
            foreach (var account in accounts)
            {
                var resolution = resolutions.GetValueOrDefault(account.UserId) ?? new PlayerResolution();
                result.Warnings.Add(resolution.Selected == null
                    ? $"账号 {account.UserId}: 未解析到角色, 玩家配置不会导入"
                    : $"账号 {account.UserId}: 将使用角色 {resolution.Selected.PlayerId} (世界 {resolution.Selected.WorldId})");

                // dry-run 也把玩家配置里不支持的字段列出来
                PreviewPlayerOptions(legacy, resolution, account.UserId, result);
            }

            return result;
        }

        // 先写设置, 最后再插入账号: 这样中途失败时数据库仍然是"没有账号",
        // 下次启动还能重新自动导入; 避免只导入了一半账号后跳过。
        foreach (var account in accounts)
        {
            try
            {
                var resolution = resolutions.GetValueOrDefault(account.UserId) ?? new PlayerResolution();
                await ImportSettingsAsync(account, resolution, legacy, result);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"账号 {account.UserId} 设置导入失败: {ex.Message}");
                _logger.LogError(ex, "导入旧账号 {UserId} 的设置失败", account.UserId);
            }
        }

        foreach (var account in accounts)
        {
            try
            {
                var resolution = resolutions.GetValueOrDefault(account.UserId) ?? new PlayerResolution();
                var currentWorldId = account.AutoLoginWorldId > 0
                    ? account.AutoLoginWorldId
                    : resolution.Selected?.WorldId;
                await UpsertAccountAsync(account, currentWorldId, resolution);
                result.AccountsImported++;
            }
            catch (Exception ex)
            {
                result.Errors.Add($"账号 {account.UserId} 插入失败: {ex.Message}");
                _logger.LogError(ex, "插入旧账号 {UserId} 失败", account.UserId);
            }
        }

        return result;
    }

    private async Task ImportSettingsAsync(
        LegacyAccountInfo account,
        PlayerResolution resolution,
        LegacyRoot legacy,
        LegacyImportResult result)
    {
        var userId = account.UserId;
        var gameConfig = legacy.GameConfig;

        // 旧版全局配置 -> 每个账号各存一份
        if (gameConfig.AutoJob != null)
        {
            await SaveAsync(userId, SettingKeys.AutoJob, MapAutoJob(gameConfig.AutoJob), result);
        }
        else
        {
            await SaveAsync(userId, SettingKeys.AutoJob, new GameConfig.AutoJobModel {DisableAll = true}, result);
            AddWarningOnce(result, "旧配置没有 AutoJob, 已写入 DisableAll=true, 避免导入后立即自动执行任务");
        }

        if (gameConfig.Shop != null)
            await SaveAsync(userId, SettingKeys.Shop, Normalize(gameConfig.Shop), result);
        if (gameConfig.DungeonBattle != null)
            await SaveAsync(userId, SettingKeys.DungeonBattle, MapDungeon(gameConfig.DungeonBattle, result), result);
        if (gameConfig.Items != null)
            await SaveAsync(userId, SettingKeys.Items, Normalize(gameConfig.Items), result);
        if (gameConfig.BountyQuestAuto != null)
            await SaveAsync(userId, SettingKeys.BountyQuestAuto, Normalize(gameConfig.BountyQuestAuto), result);
        if (gameConfig.GachaConfig != null)
            await SaveAsync(userId, SettingKeys.Gacha, Normalize(gameConfig.GachaConfig), result);
        if (gameConfig.LocalRaid != null)
            await SaveAsync(userId, SettingKeys.LocalRaid, Normalize(gameConfig.LocalRaid), result);

        // 旧版 PlayersOption -> 拆成新键名
        var (playerOption, optionPlayer) = SelectPlayerOption(legacy, resolution);
        if (playerOption != null)
        {
            if (optionPlayer != null && resolution.Selected != null && optionPlayer.PlayerId != resolution.Selected.PlayerId)
            {
                AddWarningOnce(result,
                    $"账号 {userId}: 选中角色 {resolution.Selected.PlayerId} 没有配置, 改用角色 {optionPlayer.PlayerId} 的旧配置");
            }

            if (playerOption.BattleLeague != null)
                await SaveAsync(userId, SettingKeys.BattleLeague,
                    MapPvp(playerOption.BattleLeague, $"账号{userId}.BattleLeague", result), result);
            if (playerOption.LegendLeague != null)
                await SaveAsync(userId, SettingKeys.LegendLeague,
                    MapPvp(playerOption.LegendLeague, $"账号{userId}.LegendLeague", result), result);
            if (playerOption.LocalRaid != null)
                await SaveAsync(userId, SettingKeys.LocalRaid, Normalize(playerOption.LocalRaid), result);
            if (playerOption.FriendManage != null)
                await SaveAsync(userId, SettingKeys.FriendManage, Normalize(playerOption.FriendManage), result);
            if (playerOption.GuildTower != null)
                await SaveAsync(userId, SettingKeys.GuildTower, Normalize(playerOption.GuildTower), result);
            if (playerOption.BountyQuest != null)
                await SaveAsync(userId, SettingKeys.BountyQuest, Normalize(playerOption.BountyQuest), result);
            if (playerOption.BountyQuestAuto != null)
                await SaveAsync(userId, SettingKeys.BountyQuestAuto, Normalize(playerOption.BountyQuestAuto), result);
            if (playerOption.GachaConfig != null)
                await SaveAsync(userId, SettingKeys.Gacha, Normalize(playerOption.GachaConfig), result);
        }
        else if (resolution.Selected != null)
        {
            result.Warnings.Add($"账号 {userId}: 角色 {resolution.Selected.PlayerId} 没有 PlayersOption 配置");
        }
        else
        {
            result.Warnings.Add($"账号 {userId}: 未解析到角色, 跳过 PVP/洞窟/悬赏等玩家配置");
        }

        if (resolution.Players.Count > 1)
        {
            var importedPlayerId = optionPlayer?.PlayerId ?? resolution.Selected?.PlayerId ?? 0;
            var skipped = resolution.Players
                .Where(p => p.PlayerId != importedPlayerId)
                .Select(p => p.PlayerId)
                .ToList();
            AddWarningOnce(result,
                $"账号 {userId}: 有 {resolution.Players.Count} 个角色, 只导入 {importedPlayerId} 的玩家配置, 未导入: {string.Join(", ", skipped)}");
        }
    }

    private async Task UpsertAccountAsync(LegacyAccountInfo account, long? currentWorldId, PlayerResolution resolution)
    {
        var name = string.IsNullOrWhiteSpace(account.Name) ? account.UserId.ToString() : account.Name!;
        var existing = await _fsql.Select<AccountEntity>()
            .Where(a => a.UserId == account.UserId)
            .FirstAsync();

        if (existing == null)
        {
            var entity = new AccountEntity
            {
                UserId = account.UserId,
                ClientKey = account.ClientKey!,
                Name = name,
                AutoLogin = account.AutoLogin,
                CurrentWorldId = currentWorldId,
                OrtegaUuid = resolution.Uuid ?? string.Empty,
                OrtegaAccessToken = resolution.AccessToken ?? string.Empty
            };
            await _fsql.Insert(entity).ExecuteAffrowsAsync();
            return;
        }

        existing.ClientKey = account.ClientKey!;
        if (!string.IsNullOrWhiteSpace(name)) existing.Name = name;
        existing.AutoLogin = account.AutoLogin;
        existing.CurrentWorldId ??= currentWorldId;
        if (!string.IsNullOrWhiteSpace(resolution.Uuid)) existing.OrtegaUuid = resolution.Uuid!;
        if (!string.IsNullOrWhiteSpace(resolution.AccessToken)) existing.OrtegaAccessToken = resolution.AccessToken!;
        await _fsql.Update<AccountEntity>().SetSource(existing).ExecuteAffrowsAsync();
    }

    /// <summary>
    /// 用 ClientKey 向游戏查询角色列表。失败时返回空结果, 账号和全局配置仍会导入。
    /// </summary>
    private async Task<PlayerResolution> ResolvePlayerAsync(
        LegacyAccountInfo account,
        LegacyRoot legacy,
        LegacyImportResult result)
    {
        var resolution = new PlayerResolution();
        try
        {
            // NetworkManager 由当前 DI scope 负责释放, 这里不要再手动 Dispose
            var networkManager = _serviceProvider.GetRequiredService<NetworkManager>();
            networkManager.UserId = account.UserId;
            var players = await networkManager.GetPlayerDataInfoListAsync(new LoginRequest
            {
                UserId = account.UserId,
                ClientKey = account.ClientKey!
            });

            resolution.Players = players;
            resolution.Uuid = networkManager.MoriHttpClientHandler.OrtegaUuid;
            resolution.AccessToken = networkManager.MoriHttpClientHandler.OrtegaAccessToken;

            var current = account.AutoLoginWorldId > 0
                ? players.FirstOrDefault(p => p.WorldId == account.AutoLoginWorldId)
                : null;
            var ordered = players
                .OrderByDescending(p => p.PlayerRank)
                .ThenByDescending(p => p.LastLoginTime)
                .ToList();

            resolution.Selected = current
                                   ?? ordered.FirstOrDefault(p => legacy.PlayersOption.ContainsKey(p.PlayerId.ToString()))
                                   ?? ordered.FirstOrDefault();

            if (resolution.Selected == null)
            {
                result.Warnings.Add($"账号 {account.UserId}: 游戏返回空角色列表");
            }
            else if (current == null && account.AutoLoginWorldId > 0)
            {
                result.Warnings.Add(
                    $"账号 {account.UserId}: AutoLoginWorldId={account.AutoLoginWorldId} 没有对应角色, 改用 {resolution.Selected.PlayerId}");
            }
        }
        catch (Exception ex)
        {
            result.Warnings.Add($"账号 {account.UserId}: 解析角色失败 ({ex.Message}), 只导入账号和全局配置");
            _logger.LogWarning(ex, "解析旧账号 {UserId} 的角色失败", account.UserId);
        }

        return resolution;
    }

    private static void ReportUnusedPlayerOptions(
        LegacyRoot legacy,
        IEnumerable<PlayerResolution> resolutions,
        LegacyImportResult result)
    {
        var matched = new HashSet<long>();
        foreach (var resolution in resolutions)
        {
            var (option, player) = SelectPlayerOption(legacy, resolution);
            if (option != null && player != null)
            {
                matched.Add(player.PlayerId);
            }
        }

        var unused = legacy.PlayersOption.Keys
            .Where(key => long.TryParse(key, out var id) && id != 0 && !matched.Contains(id))
            .ToList();
        if (unused.Count == 0) return;

        AddWarningOnce(result,
            $"PlayersOption 里有 {unused.Count} 个角色没有对应账号或未被选中, 未导入: {string.Join(", ", unused.Take(10))}{(unused.Count > 10 ? " ..." : "")}");
    }

    private static void PreviewPlayerOptions(
        LegacyRoot legacy,
        PlayerResolution resolution,
        long userId,
        LegacyImportResult result)
    {
        var (option, _) = SelectPlayerOption(legacy, resolution);
        if (option == null) return;

        if (option.BattleLeague != null)
            MapPvp(option.BattleLeague, $"账号{userId}.BattleLeague", result);
        if (option.LegendLeague != null)
            MapPvp(option.LegendLeague, $"账号{userId}.LegendLeague", result);
    }

    private static (LegacyPlayerOption? option, PlayerDataInfo? player) SelectPlayerOption(
        LegacyRoot legacy,
        PlayerResolution resolution)
    {
        if (resolution.Selected != null &&
            legacy.PlayersOption.TryGetValue(resolution.Selected.PlayerId.ToString(), out var selectedOption))
        {
            return (selectedOption, resolution.Selected);
        }

        foreach (var player in resolution.Players
                     .OrderByDescending(p => p.PlayerRank)
                     .ThenByDescending(p => p.LastLoginTime))
        {
            if (legacy.PlayersOption.TryGetValue(player.PlayerId.ToString(), out var option))
            {
                return (option, player);
            }
        }

        return (null, null);
    }

    private static List<LegacyAccountInfo> BuildAccounts(LegacyRoot legacy, LegacyImportResult result)
    {
        var auth = legacy.AuthOption;
        var accounts = new List<LegacyAccountInfo>();

        if (auth.Accounts is {Count: > 0})
        {
            foreach (var account in auth.Accounts)
            {
                if (account.UserId <= 0)
                {
                    result.Warnings.Add("跳过 UserId 为空的旧账号");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(account.ClientKey))
                {
                    result.Warnings.Add($"账号 {account.UserId} 没有 ClientKey, 已跳过");
                    continue;
                }

                accounts.Add(account);
            }
        }
        else if (auth.UserId > 0 && !string.IsNullOrWhiteSpace(auth.ClientKey))
        {
            accounts.Add(new LegacyAccountInfo
            {
                UserId = auth.UserId,
                ClientKey = auth.ClientKey,
                Name = "主账号",
                AutoLogin = legacy.GameConfig.Login?.AutoLogin ?? false
            });
        }

        var deduped = new Dictionary<long, LegacyAccountInfo>();
        foreach (var account in accounts)
        {
            if (deduped.ContainsKey(account.UserId))
            {
                result.Warnings.Add($"账号 {account.UserId} 重复, 使用最后一条");
            }

            deduped[account.UserId] = account;
        }

        return deduped.Values.ToList();
    }

    private static bool HasAccounts(LegacyAuthOption auth)
    {
        return auth.Accounts?.Any(a => a.UserId > 0 && !string.IsNullOrWhiteSpace(a.ClientKey)) == true
               || auth.UserId > 0 && !string.IsNullOrWhiteSpace(auth.ClientKey);
    }

    private static LegacyRoot Load(string path)
    {
        var fullPath = Path.GetFullPath(path);
        var directory = Path.GetDirectoryName(fullPath)!;
        var fileName = Path.GetFileName(fullPath);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile("appsettings.other.json", optional: true, reloadOnChange: false)
            .AddJsonFile(fileName, optional: false, reloadOnChange: false)
            .Build();

        var playersOption = new Dictionary<string, LegacyPlayerOption>();
        foreach (var child in configuration.GetSection("PlayersOption").GetChildren())
        {
            playersOption[child.Key] = child.Get<LegacyPlayerOption>() ?? new LegacyPlayerOption();
        }

        return new LegacyRoot
        {
            AuthOption = configuration.GetSection("AuthOption").Get<LegacyAuthOption>() ?? new LegacyAuthOption(),
            GameConfig = configuration.GetSection("GameConfig").Get<LegacyGameConfig>() ?? new LegacyGameConfig(),
            PlayersOption = playersOption
        };
    }

    private static GameConfig.AutoJobModel MapAutoJob(LegacyAutoJob legacy)
    {
        var json = JsonConvert.SerializeObject(legacy);
        var model = JsonConvert.DeserializeObject<GameConfig.AutoJobModel>(json) ?? new GameConfig.AutoJobModel();
        model.DisabledDailyActions ??= new List<string>();

        if (legacy.AutoReinforcementEquipmentOneTime != true)
        {
            if (!model.DisabledDailyActions.Contains(EquipmentReinforcementAction))
            {
                model.DisabledDailyActions.Add(EquipmentReinforcementAction);
            }
        }
        else
        {
            model.DisabledDailyActions.Remove(EquipmentReinforcementAction);
        }

        return model;
    }

    private static GameConfig.DungeonBattleConfig MapDungeon(
        LegacyDungeonBattle legacy,
        LegacyImportResult result)
    {
        var json = JsonConvert.SerializeObject(legacy);
        var model = JsonConvert.DeserializeObject<GameConfig.DungeonBattleConfig>(json)
                    ?? new GameConfig.DungeonBattleConfig();
        model.ShopTargetItems ??= new List<GameConfig.ShopDiscountItem>();

        if (legacy.ShopDiscountPercent is > 0)
        {
            AddDropped(result,
                $"DungeonBattle.ShopDiscountPercent={legacy.ShopDiscountPercent} 已废弃, 新版本只使用 ShopTargetItems.MinDiscountPercent");
        }

        return model;
    }

    private static PvpOption MapPvp(LegacyPvpOption? legacy, string label, LegacyImportResult result)
    {
        var option = new PvpOption();
        if (legacy == null) return option;

        option.SelectStrategy = legacy.SelectStrategy;
        option.ExcludePlayerIds = legacy.ExcludePlayerIds?.ToList() ?? new List<long>();
        option.CharacterFilters = new List<CharacterFilter>();

        foreach (var filter in legacy.CharacterFilters ?? new List<LegacyCharacterFilter>())
        {
            if (filter.FilterStrategy == LegacyCharacterFilterStrategy.Character)
            {
                option.CharacterFilters.Add(new CharacterFilter
                {
                    CharacterId = filter.CharacterId,
                    FilterStrategy = CharacterFilterStrategy.Character
                });
            }
            else
            {
                AddDropped(result,
                    $"{label}: 过滤器策略 {filter.FilterStrategy} 新版本不支持 (characterId={filter.CharacterId})");
            }

            if (filter.BattleParameterType != 0)
            {
                AddDropped(result,
                    $"{label}: BattleParameterType={filter.BattleParameterType} 新版本不支持 (characterId={filter.CharacterId})");
            }
        }

        return option;
    }

    private static void ReportLegacyOnlyFields(LegacyRoot legacy, LegacyImportResult result)
    {
        var gameConfig = legacy.GameConfig;

        if (gameConfig.DungeonBattleRelicSort is {Count: > 0})
        {
            AddDropped(result, $"DungeonBattleRelicSort ({gameConfig.DungeonBattleRelicSort.Count} 条) 新版本不支持");
        }

        if (gameConfig.DungeonBattle?.ShopDiscountPercent is > 0)
        {
            AddDropped(result,
                $"DungeonBattle.ShopDiscountPercent={gameConfig.DungeonBattle.ShopDiscountPercent} 已废弃, 新版本只使用 ShopTargetItems.MinDiscountPercent");
        }

        if (gameConfig.RecordBattleLog == false)
        {
            AddDropped(result, "RecordBattleLog=false 新版本固定记录战斗日志到数据库");
        }

        if (gameConfig.ReportBattleLog == false)
        {
            AddDropped(result, "ReportBattleLog=false 新版本无对应项");
        }

        if (!string.IsNullOrWhiteSpace(gameConfig.BattleLogDir) &&
            !string.Equals(gameConfig.BattleLogDir, "BattleLogs/", StringComparison.OrdinalIgnoreCase))
        {
            AddDropped(result, $"BattleLogDir={gameConfig.BattleLogDir} 新版本固定使用数据库");
        }

        if (!string.IsNullOrWhiteSpace(gameConfig.ServerUrl))
        {
            AddDropped(result, $"ServerUrl={gameConfig.ServerUrl} 新版本无对应项");
        }

        if (gameConfig.AutoRequestDelay is > 0)
        {
            AddDropped(result, $"AutoRequestDelay={gameConfig.AutoRequestDelay} 请改用环境变量 Game__AutoRequestDelay");
        }

        if (!string.IsNullOrWhiteSpace(legacy.AuthOption.OSVersion) ||
            !string.IsNullOrWhiteSpace(legacy.AuthOption.ModelName) ||
            !string.IsNullOrWhiteSpace(legacy.AuthOption.DeviceToken))
        {
            AddDropped(result, "AuthOption.OSVersion/ModelName/DeviceToken 新版本不再读取");
        }

        if (legacy.PlayersOption.Values.Any(p => p.QuickActionSwitch is {Count: > 0}))
        {
            AddDropped(result, "PlayerOption.QuickActionSwitch 旧版本未实际使用, 已忽略");
        }
    }

    private static T Normalize<T>(T value) where T : class
    {
        switch (value)
        {
            case GameConfig.ShopConfig shop:
                shop.AutoBuyItems ??= new List<GameConfig.ShopAutoBuyItem>();
                break;
            case GameConfig.ItemsConfig items:
                items.AutoUseItemTypes ??= new List<GameConfig.AutoUseItemType>();
                break;
            case GameConfig.BountyQuestAutoConfig bountyAuto:
                bountyAuto.TargetItems ??= new List<MementoMori.Ortega.Share.Data.Item.UserItem>();
                break;
            case GameConfig.GachaConfigModel gacha:
                gacha.AutoGachaConsumeUserItems ??= new List<MementoMori.Ortega.Share.Data.Item.UserItem>();
                break;
            case GameConfig.LocalRaidConfig localRaid:
                localRaid.RewardItems ??= new List<GameConfig.WeightedItem>();
                break;
            case FriendManageOption friendManage:
                friendManage.AutoRemoveWhitelist ??= new List<long>();
                break;
            case BountyQuestOption bountyQuest:
                bountyQuest.TodayRefreshCount ??= new Dictionary<string, int>();
                break;
        }

        return value;
    }

    private async Task SaveAsync<T>(long userId, string key, T value, LegacyImportResult result) where T : class
    {
        await _settings.SaveSettingAsync(userId, key, value);
        if (result.WrittenSettings.Add((userId, key)))
        {
            result.SettingsImported++;
        }
    }

    private static void AddDropped(LegacyImportResult result, string message)
    {
        if (!result.Dropped.Contains(message)) result.Dropped.Add(message);
    }

    private static void AddWarningOnce(LegacyImportResult result, string message)
    {
        if (!result.Warnings.Contains(message)) result.Warnings.Add(message);
    }

    private sealed class PlayerResolution
    {
        public List<PlayerDataInfo> Players { get; set; } = new();
        public PlayerDataInfo? Selected { get; set; }
        public string? Uuid { get; set; }
        public string? AccessToken { get; set; }
    }
}

public class LegacyImportResult
{
    public string SourcePath { get; set; } = string.Empty;
    public bool DryRun { get; set; }
    public int AccountsFound { get; set; }
    public int AccountsImported { get; set; }
    public int SettingsImported { get; set; }
    public HashSet<(long UserId, string Key)> WrittenSettings { get; } = new();
    public List<string> Warnings { get; } = new();
    public List<string> Dropped { get; } = new();
    public List<string> Errors { get; } = new();

    public string ToSummary()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"legacy import{(DryRun ? " (dry-run)" : "")}: {SourcePath}");
        sb.AppendLine($"accounts: found={AccountsFound}, imported={AccountsImported}, settings={SettingsImported}");

        AppendSection(sb, "warnings", Warnings);
        AppendSection(sb, "not imported", Dropped);
        AppendSection(sb, "errors", Errors);

        return sb.ToString().TrimEnd();
    }

    private static void AppendSection(StringBuilder sb, string title, List<string> items)
    {
        if (items.Count == 0) return;

        sb.AppendLine($"{title}:");
        foreach (var item in items.Take(20))
        {
            sb.AppendLine($"  - {item}");
        }

        if (items.Count > 20)
        {
            sb.AppendLine($"  ... 另有 {items.Count - 20} 条");
        }
    }
}
