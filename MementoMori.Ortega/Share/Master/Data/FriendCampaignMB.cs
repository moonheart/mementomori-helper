using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Master.Interfaces;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("フレンドキャンペーン")]
    public class FriendCampaignMB : MasterBookBase, IHasStartEndTime, ICharacterImage
    {
        [PropertyOrder(3)]
        [Description("コード入力有効期間")]
        public int CodeExpirationPeriod { get; }

        [DateTimeString]
        [PropertyOrder(4)]
        [Description("コード入力開始時間")]
        public string CodeStartTime { get; }

        [PropertyOrder(11)]
        [Description("招待コード入力上限数")]
        public int CodeLimitCount { get; }

        [PropertyOrder(9)]
        [Description("対象フレンドミッションリスト")]
        public IReadOnlyList<long> FriendMissionIdList { get; }

        [Nest(false, 0)]
        [PropertyOrder(12)]
        [Description("招待コード入力報酬リスト")]
        public IReadOnlyList<UserItem> RewardItemList { get; }

        [PropertyOrder(10)]
        [Description("キャンペーンタイトル")]
        public string Title { get; }

        [SerializationConstructor]
        public FriendCampaignMB(long id, bool? isIgnore, string memo, string startTime, string endTime, int codeExpirationPeriod, string codeStartTime, long characterImageId, float characterImageX, float characterImageY,
            float characterImageSize, IReadOnlyList<long> friendMissionIdList, string title, int codeLimitCount, IReadOnlyList<UserItem> rewardItemList)
            : base(id, isIgnore, memo)
        {
            StartTime = startTime;
            EndTime = endTime;
            CodeExpirationPeriod = codeExpirationPeriod;
            CodeStartTime = codeStartTime;
            CharacterImageId = characterImageId;
            CharacterImageX = characterImageX;
            CharacterImageY = characterImageY;
            CharacterImageSize = characterImageSize;
            FriendMissionIdList = friendMissionIdList;
            Title = title;
            CodeLimitCount = codeLimitCount;
            RewardItemList = rewardItemList;
        }

        public FriendCampaignMB() : base(0, false, "")
        {
        }

        [PropertyOrder(5)]
        [Description("キャラ画像Id")]
        public long CharacterImageId { get; }

        [PropertyOrder(6)]
        [Description("キャラ画像座標X")]
        public float CharacterImageX { get; }

        [PropertyOrder(7)]
        [Description("キャラ画像座標Y")]
        public float CharacterImageY { get; }

        [PropertyOrder(8)]
        [Description("キャラ画像サイズ")]
        public float CharacterImageSize { get; }

        [PropertyOrder(2)]
        [Description("終了時刻")]
        public string EndTime { get; }

        [PropertyOrder(1)]
        [Description("開始時刻")]
        public string StartTime { get; }
    }
}