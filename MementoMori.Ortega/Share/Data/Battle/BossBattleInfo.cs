using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
    [MessagePackObject(true)]
    [Description("ボス情報")]
    public class BossBattleInfo
    {
        [Description("クエストId(QuestMB)")]
        public long QuestId { get; set; }

        [Description("ドロップアイテムリスト")]
        public List<UserItem> BossBattleDropItems { get; set; }

        [Description("ボスバトル確定ドロップアイテムリスト")]
        public List<UserItem> FixedBossBattleDropItems { get; set; }

        [Description("ボスバトル初回ドロップアイテムリスト")]
        public List<UserItem> FirstBossBattleDropItems { get; set; }

        [Description("ボス表示情報リスト")]
        public List<BossDisplayInfo> BossDisplayInfos { get; set; }
    }
}