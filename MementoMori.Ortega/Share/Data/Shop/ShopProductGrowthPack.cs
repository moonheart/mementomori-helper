using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Shop
{
	[MessagePackObject(true)]
	public class ShopProductGrowthPack
	{
		[Description("キャラクターID")]
		public long CharacterId { get; set; }

		[Description("終了日時")]
		public string EndTime { get; set; }

        [Description("成長目標の不可視レアリティ")]
		public CharacterRarityFlags InvisibleStartRarityFlag { get; set; }

		[Description("名称キー")]
		public string NameKey { get; set; }

        [Description("成長リスト")]
		public IReadOnlyList<ShopGrowthPackGoalDetail> ShopGrowthPackGoalDetailList { get; set; }
    }
}
