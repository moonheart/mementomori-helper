using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("GvG修練レベル")]
	public class GvGServerLvMB : MasterBookBase
	{
		[PropertyOrder(2)]
		[Description("経過日数")]
		public int ElapsedDays
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("修練レベル")]
		public long Lv
		{
			get;
		}

		[Description("グランドバトル神殿上位最小配置数")]
		[PropertyOrder(6)]
		public int MinCharacterNumGrandBattleLargeGolden
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("グランドバトル教会上位最小配置数")]
		public int MinCharacterNumGrandBattleMediumGolden
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("グランドバトル城上位最小配置数")]
		public int MinCharacterNumGrandBattleSmallGolden
		{
			get;
		}

		[Description("グランドバトル神殿中位最小配置数")]
		[PropertyOrder(9)]
		public int MinCharacterNumGrandBattleLargeSilver
		{
			get;
		}

		[Description("グランドバトル教会中位最小配置数")]
		[PropertyOrder(10)]
		public int MinCharacterNumGrandBattleMediumSilver
		{
			get;
		}

		[Description("グランドバトル城中位最小配置数")]
		[PropertyOrder(11)]
		public int MinCharacterNumGrandBattleSmallSilver
		{
			get;
		}

		[Description("グランドバトル神殿下位最小配置数")]
		[PropertyOrder(12)]
		public int MinCharacterNumGrandBattleLargeBronze
		{
			get;
		}

		[PropertyOrder(13)]
		[Description("グランドバトル教会下位最小配置数")]
		public int MinCharacterNumGrandBattleMediumBronze
		{
			get;
		}

		[Description("グランドバトル城下位最小配置数")]
		[PropertyOrder(14)]
		public int MinCharacterNumGrandBattleSmallBronze
		{
			get;
		}

		[Description("ギルドバトル神殿最小配置数")]
		[PropertyOrder(3)]
		public int MinCharacterNumGuildBattleLarge
		{
			get;
		}

		[Description("ギルドバトル教会最小配置数")]
		[PropertyOrder(4)]
		public int MinCharacterNumGuildBattleMedium
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("ギルドバトル城最小配置数")]
		public int MinCharacterNumGuildBattleSmall
		{
			get;
		}

		[SerializationConstructor]
		public GvGServerLvMB(long id, bool? isIgnore, string memo, int elapsedDays, long lv, int minCharacterNumGrandBattleLargeGolden, int minCharacterNumGrandBattleMediumGolden, int minCharacterNumGrandBattleSmallGolden, int minCharacterNumGrandBattleLargeSilver, int minCharacterNumGrandBattleMediumSilver, int minCharacterNumGrandBattleSmallSilver, int minCharacterNumGrandBattleLargeBronze, int minCharacterNumGrandBattleMediumBronze, int minCharacterNumGrandBattleSmallBronze, int minCharacterNumGuildBattleLarge, int minCharacterNumGuildBattleMedium, int minCharacterNumGuildBattleSmall)
			:base(id, isIgnore, memo)
		{
			ElapsedDays = elapsedDays;
			Lv = lv;
			MinCharacterNumGrandBattleLargeGolden = minCharacterNumGrandBattleLargeGolden;
			MinCharacterNumGrandBattleMediumGolden = minCharacterNumGrandBattleMediumGolden;
			MinCharacterNumGrandBattleSmallGolden = minCharacterNumGrandBattleSmallGolden;
			MinCharacterNumGrandBattleLargeSilver = minCharacterNumGrandBattleLargeSilver;
			MinCharacterNumGrandBattleMediumSilver = minCharacterNumGrandBattleMediumSilver;
			MinCharacterNumGrandBattleSmallSilver = minCharacterNumGrandBattleSmallSilver;
			MinCharacterNumGrandBattleLargeBronze = minCharacterNumGrandBattleLargeBronze;
			MinCharacterNumGrandBattleMediumBronze = minCharacterNumGrandBattleMediumBronze;
			MinCharacterNumGrandBattleSmallBronze = minCharacterNumGrandBattleSmallBronze;
			MinCharacterNumGuildBattleLarge = minCharacterNumGuildBattleLarge;
			MinCharacterNumGuildBattleMedium = minCharacterNumGuildBattleMedium;
			MinCharacterNumGuildBattleSmall = minCharacterNumGuildBattleSmall;
		}

		public GvGServerLvMB():base(0, false, string.Empty)
		{
		}
	}
}
