using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.TowerBattle
{
	[OrtegaApi("towerBattle/start", true, false)]
	[MessagePackObject(true)]
	public class StartRequest : ApiRequestBase
	{
		public bool IsQuick
		{
			get;
			set;
		}

		public TowerType TargetTowerType
		{
			get;
			set;
		}

		public long TowerBattleQuestId
		{
			get;
			set;
		}

		public StartRequest()
		{
		}
	}
}
