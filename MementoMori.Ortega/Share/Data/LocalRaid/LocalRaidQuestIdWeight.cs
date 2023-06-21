using System.ComponentModel;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid
{
	[MessagePackObject(true)]
	public class LocalRaidQuestIdWeight : ILotteryWeight
	{
		[PropertyOrder(1)]
		[Description("幻影の神殿のクエストId")]
		public long LocalRaidQuestId
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		[Description("出現割合")]
		public int LotteryWeight
		{
			get;
			set;
		}

		public LocalRaidQuestIdWeight()
		{
		}
	}
}
