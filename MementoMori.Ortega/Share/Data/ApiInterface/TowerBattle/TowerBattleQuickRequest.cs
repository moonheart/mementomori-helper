using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle
{
	[OrtegaApi("towerBattle/quick", true, false)]
	[MessagePackObject(true)]
	public class TowerBattleQuickRequest : ApiRequestBase
	{
		public TowerType TargetTowerType { get; set; }

		public long TowerBattleQuestId { get; set; }

		public int QuickCount { get; set; }
	}
}
