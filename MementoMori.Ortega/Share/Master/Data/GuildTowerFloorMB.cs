using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("ギルドツリー階層")]
    public class GuildTowerFloorMB : MasterBookBase
    {
        [PropertyOrder(5)]
        [Description("ベース個人戦力目安")]
        public long BaseClearPartyBattlePower { get; }

        [PropertyOrder(8)]
        [Description("ベース敵IDリスト")]
        public IReadOnlyList<long> BaseEnemyIdList { get; }

        [PropertyOrder(6)]
        [Description("ベース強化素材数")]
        public IReadOnlyList<int> BaseReinforcementMaterialCountList { get; }

        [PropertyOrder(1)]
        [Description("イベント番号")]
        public int EventNo { get; }

        [PropertyOrder(2)]
        [Description("階層数")]
        public int FloorCount { get; }

        [Nest(false, 0)]
        [PropertyOrder(7)]
        [Description("階層報酬リスト")]
        public IReadOnlyList<UserItem> FloorRewardList { get; }

        [PropertyOrder(4)]
        [Description("選択可能難易度")]
        public IReadOnlyList<int> SelectableDifficultyList { get; }

        [PropertyOrder(3)]
        [Description("クリア必要星数")]
        public int RequiredProgressCount { get; }

        [SerializationConstructor]
        public GuildTowerFloorMB(long id, bool? isIgnore, string memo, int eventNo, int floorCount, int requiredProgressCount, IReadOnlyList<int> selectableDifficultyList,
            long baseClearPartyBattlePower, IReadOnlyList<int> baseReinforcementMaterialCountList, IReadOnlyList<UserItem> floorRewardList, IReadOnlyList<long> baseEnemyIdList)
            : base(id, isIgnore, memo)
        {
            EventNo = eventNo;
            FloorCount = floorCount;
            RequiredProgressCount = requiredProgressCount;
            SelectableDifficultyList = selectableDifficultyList;
            BaseClearPartyBattlePower = baseClearPartyBattlePower;
            BaseReinforcementMaterialCountList = baseReinforcementMaterialCountList;
            FloorRewardList = floorRewardList;
            BaseEnemyIdList = baseEnemyIdList;
        }

        public GuildTowerFloorMB() : base(default, default, default)
        {
        }
    }
}