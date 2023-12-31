using MementoMori.Ortega.Share.Data.Battle.Result;
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
}