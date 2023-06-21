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
		public int CodeExpirationPeriod
		{
			get;
		}

		[Description("招待コード入力上限数")]
		[PropertyOrder(10)]
		public int CodeLimitCount
		{
			get;
		}

		[Description("対象フレンドミッションリスト")]
		[PropertyOrder(8)]
		public IReadOnlyList<long> FriendMissionIdList
		{
			get;
		}

		[Nest(false, 0)]
		[PropertyOrder(11)]
		[Description("招待コード入力報酬リスト")]
		public IReadOnlyList<UserItem> RewardItemList
		{
			get;
		}

		[PropertyOrder(9)]
		[Description("キャンペーンタイトル")]
		public string Title
		{
			get;
		}

		[SerializationConstructor]
		public FriendCampaignMB(long id, bool? isIgnore, string memo, string startTime, string endTime, int codeExpirationPeriod, long characterImageId, float characterImageX, float characterImageY, float characterImageSize, IReadOnlyList<long> friendMissionIdList, string title, int codeLimitCount, IReadOnlyList<UserItem> rewardItemList)
			:base(id, isIgnore, memo)
		{
			StartTime = startTime;
			EndTime = endTime;
			CodeExpirationPeriod = codeExpirationPeriod;
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

		[PropertyOrder(4)]
		[Description("キャラ画像Id")]
		public long CharacterImageId
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("キャラ画像座標X")]
		public float CharacterImageX
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("キャラ画像座標Y")]
		public float CharacterImageY
		{
			get;
		}

		[Description("キャラ画像サイズ")]
		[PropertyOrder(7)]
		public float CharacterImageSize
		{
			get;
		}

		[Description("終了時刻")]
		[PropertyOrder(2)]
		public string EndTime
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("開始時刻")]
		public string StartTime
		{
			get;
		}
	}
}
