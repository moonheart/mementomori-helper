using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildDamageBarReward
	{
		public int DamageBarCount { get; set; }

		[Nest(true, 1)]
		public List<UserItem> GuildRewardItems{ get; set; }
	}
}
