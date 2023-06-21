using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("時空の洞窟 味方情報")]
	[MessagePackObject(true)]
	public class DungeonBattleAllyInfo
	{
		[Description("レアリティ")]
		public CharacterRarityFlags CharacterRarityFlags
		{
			get;
			set;
		}

		[Description("残りのHP（‰）")]
		public int CurrentHpPerMill
		{
			get;
			set;
		}

		[Description("属性")]
		public ElementType ElementType
		{
			get;
			set;
		}

		[Description("BattleCharacterReport.BattleCharacterGuid")]
		public int Guid
		{
			get;
			set;
		}

		[Description("ユニットアイコンID")]
		public long UnitIconId
		{
			get;
			set;
		}

		[Description("ユニットアイコンタイプ")]
		public UnitIconType UnitIconType
		{
			get;
			set;
		}

		public DungeonBattleAllyInfo()
		{
		}
	}
}
