using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
	[MessagePackObject(true)]
	[Description("武具強化必要アイテム情報")]
	public class EquipmentReinforcementMaterialInfo
	{
		[Description("レベル")]
		[PropertyOrder(1)]
		public long Lv
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		[Nest(true, 1)]
		[Description("必要アイテム")]
		public List<UserItem> RequiredItemList
		{
			get;
			set;
		}

		public EquipmentReinforcementMaterialInfo()
		{
		}
	}
}
