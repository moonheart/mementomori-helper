using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DungeonBattle
{
	[MessagePackObject(true)]
	public class DungeonBattleCacheInfo
	{
		public Dictionary<int, DungeonBattleCacheInfo.CharacterCache> Enemies
		{
			get;
			set;
		}

		public List<UserItem> RewardNormalItems
		{
			get;
			set;
		}

		public List<long> RewardRelicIds
		{
			get;
			set;
		}

		public List<UserItem> RewardSpecialItems
		{
			get;
			set;
		}

		public DungeonBattleCacheInfo()
		{
			this.Enemies = new Dictionary<int, CharacterCache>();
		}
 
		[MessagePackObject(true)]
		public class CharacterCache
		{
			public long CurrentHp
			{
				get;
				set;
			}

			public CharacterCache()
			{
			}
		}
	}
}
