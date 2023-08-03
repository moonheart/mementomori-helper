using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	[Obsolete("1.2.9で削除予定")]
	public class GuildRaidNormalDamageBarReward : GuildRaidDamageBarReward
	{
		[Nest(true, 1)]
		public GuildRaidGoldCoefficientInfo RewardGoldCoefficientInfo{ get; set; }
	}
}
