using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
	[Description("必要アイテム情報")]
	[MessagePackObject(true)]
	public class EquipmentEvolutionInfo
	{
		[Description("レアリティ")]
		[PropertyOrder(1)]
		public EquipmentRarityFlags RarityFlags
		{
			get;
			set;
		}

		[Description("進化前武具レベル")]
		[PropertyOrder(2)]
		public long BeforeEquipmentLv
		{
			get;
			set;
		}

		[Description("進化後武具レベル")]
		[PropertyOrder(3)]
		public long AfterEquipmentLv
		{
			get;
			set;
		}

		[PropertyOrder(4)]
		[Nest(true, 1)]
		[Description("必要アイテムリスト")]
		public List<UserItem> RequiredItemList
		{
			get;
			set;
		}

		public EquipmentEvolutionInfo()
		{
		}
	}
}
