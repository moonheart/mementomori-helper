using MementoMori.Ortega.Share.Data.Battle.Result;
using MementoMori.Common.Localization;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace MementoMori.BlazorShared.Pages;

public partial class BattleLog
{
    private List<string> BattleResults { get; set; } = new();

    private BattleLogType selectedBattleLogType
    {
        get => _selectedBattleLogType;
        set
        {
            _selectedBattleLogType = value;
            this.GetLogs();
        }
    }

    private string SelectedBattleResult { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (!Directory.Exists(GameConfig.Value.BattleLogDir))
        {
            return;
        }

        GetLogs();
    }

    private void GetLogs()
    {
        var files = Directory.GetFiles(GameConfig.Value.BattleLogDir);
        var prefix = logTypePrefixes[selectedBattleLogType];
        BattleResults.Clear();
        foreach (var file in files.OrderDescending())
        {
            var fileName = Path.GetFileName(file);
            if (!fileName.StartsWith(prefix)) continue;
            BattleResults.Add(fileName);
        }

        SelectedBattleResult = BattleResults.FirstOrDefault();
    }

    private async Task DownloadBattleLog(string filename)
    {
        if (string.IsNullOrEmpty(filename)) return;
        if (!ReadLogContent(filename, out var json)) return;
        try
        {
            await FileSaver.SaveFile(json, filename);
        }
        catch (Exception e)
        {
            await DialogService.ShowMessageBox("错误", e.ToString());
        }
    }

    private bool ReadLogContent(string filename, out string json)
    {
        try
        {
            json = null;
            if (string.IsNullOrEmpty(filename)) return false;
            var path = Path.Combine(GameConfig.Value.BattleLogDir, filename);
            var content = File.ReadAllText(path);
            var battleResult = JsonConvert.DeserializeObject<BattleResult>(content);
            if (battleResult == null)
            {
                return false;
            }

            var obj = new
            {
                BattleSimulationResult = battleResult.SimulationResult
            };
            json = JsonConvert.SerializeObject(obj);
            return true;
        }
        catch (Exception e)
        {
            DialogService.ShowMessageBox("错误", e.ToString());
            json = null;
            return false;
        }
    }

    private async Task ViewBattleLog(string filename)
    {
        if (string.IsNullOrEmpty(filename)) return;
        if (!ReadLogContent(filename, out var json)) return;
        await JS.InvokeVoidAsync("showBattleLog", json);
    }

    public enum BattleLogType
    {
        Main,
        TowerInfinite,
        TowerBlue,
        TowerRed,
        TowerGreen,
        TowerYellow,
        BattleLeague,
        LegendLeague
    }

    private Dictionary<BattleLogType, string> logTypePrefixes = new()
    {
        {BattleLogType.Main, "main-"},
        {BattleLogType.TowerInfinite, "tower-Infinite-"},
        {BattleLogType.TowerBlue, "tower-Blue-"},
        {BattleLogType.TowerRed, "tower-Red-"},
        {BattleLogType.TowerGreen, "tower-Green-"},
        {BattleLogType.TowerYellow, "tower-Yellow-"},
        {BattleLogType.BattleLeague, "battleleague-"},
        {BattleLogType.LegendLeague, "legendleague-"},
    };

    private BattleLogType _selectedBattleLogType = BattleLogType.Main;
    private int _selectedCleanRange = 1;

    private DateTimeOffset ParseTimeFromFileName(string filename)
    {
        var parts = filename.Split('-');
        if (parts.Length < 5) return DateTimeOffset.MinValue;
        if (long.TryParse(parts[3], out var timestamp))
            return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).ToLocalTime();
        return DateTimeOffset.MinValue;
    }

    private async Task CleanBattleLogs()
    {
        if (!Directory.Exists(GameConfig.Value.BattleLogDir)) return;

        var threshold = _selectedCleanRange switch
        {
            1 => DateTimeOffset.Now.AddDays(-1),
            7 => DateTimeOffset.Now.AddDays(-7),
            30 => DateTimeOffset.Now.AddDays(-30),
            _ => DateTimeOffset.MinValue
        };

        var filesToDelete = _selectedCleanRange == 0
            ? Directory.GetFiles(GameConfig.Value.BattleLogDir).ToList()
            : Directory.GetFiles(GameConfig.Value.BattleLogDir)
                .Where(f => ParseTimeFromFileName(Path.GetFileName(f)) < threshold)
                .ToList();

        if (!filesToDelete.Any())
        {
            await DialogService.ShowMessageBox(ResourceStrings.BattleLogNoFilesToClean, ResourceStrings.BattleLogNoFilesToClean);
            return;
        }

        long totalSize = filesToDelete.Sum(f => new FileInfo(f).Length);

        string FormatFileSize(long bytes)
        {
            string[] sizes = {"B", "KB", "MB", "GB"};
            int order = 0;
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                order++;
                bytes /= 1024;
            }

            return $"{bytes:0.##} {sizes[order]}";
        }

        var result = await DialogService.ShowMessageBox(
            ResourceStrings.BattleLogCleanRange,
            string.Format(ResourceStrings.BattleLogCleanConfirm, filesToDelete.Count, FormatFileSize(totalSize)),
            yesText: ResourceStrings.Confirm, cancelText: ResourceStrings.Cancel);

        if (result != true) return;

        foreach (var file in filesToDelete)
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception ex)
            {
                await DialogService.ShowMessageBox(ResourceStrings.BattleLogCleanError,
                    string.Format(ResourceStrings.BattleLogCleanError, file, ex.Message));
            }
        }

        GetLogs();
        await DialogService.ShowMessageBox(ResourceStrings.Finished,
            string.Format(ResourceStrings.BattleLogCleanComplete, filesToDelete.Count));
    }
}