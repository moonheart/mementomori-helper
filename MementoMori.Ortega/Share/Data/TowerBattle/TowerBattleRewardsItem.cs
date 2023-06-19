using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.TowerBattle
{
	[MessagePackObject(true)]
	public class TowerBattleRewardsItem
	{
		public UserItem Item
		{
			get;
			set;
		}

		public TowerBattleRewardsType RewardsType
		{
			get;
			set;
		}

		public TowerBattleRewardsItem()
		{
		}
	}
}
