using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("月間ログインボーナス")]
	public class MonthlyLoginBonusMB : MasterBookBase
	{
		[Description("画像Id")]
		[PropertyOrder(3)]
		public int ImageId
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("報酬リストID")]
		public long RewardListId
		{
			get;
		}

		[Description("年月")]
		[PropertyOrder(1)]
		public string YearMonth
		{
			get;
		}

		[SerializationConstructor]
		public MonthlyLoginBonusMB(long id, bool? isIgnore, string memo, string yearMonth, long rewardListId, int imageId)
			:base(id, isIgnore, memo)
		{
			YearMonth = yearMonth;
			RewardListId = rewardListId;
			ImageId = imageId;
		}

		public MonthlyLoginBonusMB() :base(0L, false, ""){}
	}
}
