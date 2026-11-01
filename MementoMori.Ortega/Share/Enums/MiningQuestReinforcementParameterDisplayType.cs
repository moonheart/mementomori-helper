using System.ComponentModel;

public enum MiningQuestReinforcementParameterDisplayType
{
    [Description("None")]
    None,
    [Description("通常表示")]
    Normal,
    [Description("出現する美術品レベル")]
    SpawnBlockLevel = 101,
    [Description("美術品リポップ率")]
    RespawnBlockLevel,
    [Description("ブロックALv")]
    BlockALevel = 201,
    [Description("ブロックBLv")]
    BlockBLevel,
    [Description("ブロックCLv")]
    BlockCLevel,
    [Description("ブロックDLv")]
    BlockDLevel
}
