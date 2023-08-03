using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class NormalDamageBarReward
	{
		[Nest(true, 1)]
		public GuildRaidGoldCoefficientInfo NormalRewardGoldCoefficientInfo{ get; set; }

		public int DamageBarCount { get; set; }

		[Nest(true, 1)]
		public List<UserItem> NormalRewardItems{ get; set; }
	}
}
