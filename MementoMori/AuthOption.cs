using MementoMori.Ortega.Share.Data.Item;

namespace MementoMori;

public class AuthOption
{
    public string ClientKey { get; set; }
    public string DeviceToken { get; set; }
    public string AppVersion { get; set; }
    public string OSVersion { get; set; }
    public string ModelName { get; set; }
    public string AdverisementId { get; set; }
    public long UserId { get; set; }

    public Dictionary<string, string> Headers { get; set; }
}

public class GameConfig
{
    public class DungeonBattleRelicSortInfo
    {
        public long Id { get; set; }
        public string Desc { get; set; }
    }

    public class GachaConfigModel
    {
        public List<UserItem> AutoGachaConsumeUserItems { get; set; } = new();
    }

    public GachaConfigModel GachaConfig { get; set; }
    public DungeonBattleRelicSortInfo[] DungeonBattleRelicSort { get; set; }
}