using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("ランキング到達報酬")]
    public class AchieveRankingRewardMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("到達目標説明キー")]
        public string AchieveTargetDescriptionKey { get; }

        [PropertyOrder(2)]
        [Description("ランキング種別")]
        public RankingDataType RankingDataType { get; }

        [PropertyOrder(3)]
        [Description("達成に必要な値")]
        public long RequireValue { get; }

        [Nest(false, 0)]
        [PropertyOrder(4)]
        [Description("報酬アイテムリスト")]
        public IReadOnlyList<UserItem> RewardItemList { get; }

        [SerializationConstructor]
        public AchieveRankingRewardMB(long id, bool? isIgnore, string memo, string achieveTargetDescriptionKey, RankingDataType rankingDataType, long requireValue,
            IReadOnlyList<UserItem> rewardItemList)
            : base(id, isIgnore, memo)
        {
            AchieveTargetDescriptionKey = achieveTargetDescriptionKey;
            RankingDataType = rankingDataType;
            RequireValue = requireValue;
            RewardItemList = rewardItemList;
        }

        public AchieveRankingRewardMB() : base(0, null, null)
        {
        }
    }
}