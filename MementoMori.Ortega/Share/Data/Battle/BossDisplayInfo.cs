using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle;

[MessagePackObject(true)]
[Description("ボス表示情報")]
public class BossDisplayInfo
{
    [Description("ボス名称")]
    public string NameKey { get; set; }

    [Description("ボスId(BattleEnemyMB)")]
    public long BossEnemyId { get; set; }
}