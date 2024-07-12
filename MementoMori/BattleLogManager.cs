using AutoCtor;
using Injectio.Attributes;
using MementoMori.Option;
using MementoMori.Ortega.Share.Data.Battle.Result;

namespace MementoMori;

[AutoConstruct]
[RegisterSingleton<BattleLogManager>]
public partial class BattleLogManager
{
    private readonly IWritableOptions<GameConfig> _writableGameConfig;

    public async Task SaveBattleLog(BattleResult battleResult, string prefix, string middle, string autoDeletePrefix = null, int autoDeletePreserveCount = 20)
    {
        if (_writableGameConfig.Value.RecordBattleLog)
        {
            Directory.CreateDirectory(_writableGameConfig.Value.BattleLogDir);
            var res = battleResult.SimulationResult.BattleEndInfo.IsWinAttacker() ? "win" : "lose";
            var filename = $"{prefix}-{middle}-{res}-{battleResult.BattleTime.StartBattle}-{battleResult.SimulationResult.BattleToken}.json";
            filename = Path.GetInvalidFileNameChars().Aggregate(filename, (current, invalidFileNameChar) => current.Replace(invalidFileNameChar, '_'));
            var path = Path.Combine(_writableGameConfig.Value.BattleLogDir, filename);
            await File.WriteAllTextAsync(path, battleResult.ToJson(true));
            // keep only 100 lose logs
            var files = Directory.GetFiles(_writableGameConfig.Value.BattleLogDir, $"{autoDeletePrefix ?? prefix}-*.json").OrderDescending();
            foreach (var file in files.Skip(autoDeletePreserveCount))
            {
                File.Delete(file);
            }
        }
    }
}