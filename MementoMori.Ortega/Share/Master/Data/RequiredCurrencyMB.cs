using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("必要仮想通貨")]
	public class RequiredCurrencyMB : MasterBookBase
	{
		[PropertyOrder(3)]
		[Description("ボス挑戦回数の購入に必要な仮想通貨")]
		public long BossBattle
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("回数")]
		public long Count
		{
			get;
		}

		[Description("レジェンドリーグ挑戦回数に必要な仮想通貨")]
		[PropertyOrder(5)]
		public long LegendLeague
		{
			get;
		}

		[Description("PvP挑戦回数に必要な仮想通貨")]
		[PropertyOrder(4)]
		public long Pvp
		{
			get;
		}

		[Description("高速周回に必要な仮想通貨")]
		[PropertyOrder(2)]
		public long Quick
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("訓練所挑戦回数の購入に必要な仮想通貨")]
		public long TowerBattle
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("レベルリンク枠開放に必要な仮想通貨")]
		public long LevelLinkMember
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("団結ミッション受注回数購入に必要な仮想通貨")]
		public long UnionMission
		{
			get;
		}

		[SerializationConstructor]
		public RequiredCurrencyMB(long id, bool? isIgnore, string memo, long bossBattle, long count, long legendLeague, long pvp, long quick, long towerBattle, long levelLinkMember, long unionMission)
			:base(id, isIgnore, memo)
		{
			BossBattle = bossBattle;
			Count = count;
			LegendLeague = legendLeague;
			Pvp = pvp;
			Quick = quick;
			TowerBattle = towerBattle;
			LevelLinkMember = levelLinkMember;
			UnionMission = unionMission;
		}

		public RequiredCurrencyMB() :base(0L, false, ""){}

	}
}
