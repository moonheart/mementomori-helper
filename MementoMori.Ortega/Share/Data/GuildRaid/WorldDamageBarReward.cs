using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class WorldDamageBarReward
	{
		public long GoalDamage
		{
			get;
			set;
		}

		[Nest(true, 1)]
		public List<UserItem> WorldRewardItems
		{
			get;
			set;
		}

		public WorldDamageBarReward()
		{
		}
	}
}
