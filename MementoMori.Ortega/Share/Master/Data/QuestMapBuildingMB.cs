using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("放置バトルマップオブジェクト")]
	public class QuestMapBuildingMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("デフォルトタイプID")]
		public long DefaultTypeId
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("選択タイプID 1")]
		public long SelectTypeId1
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("選択タイプID 2")]
		public long SelectTypeId2
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("選択タイプID 3")]
		public long SelectTypeId3
		{
			get;
		}

		[SerializationConstructor]
		public QuestMapBuildingMB(long id, bool? isIgnore, string memo, long defaultTypeId, long selectTypeId1, long selectTypeId2, long selectTypeId3)
			:base(id, isIgnore, memo)
		{
			DefaultTypeId = defaultTypeId;
			SelectTypeId1 = selectTypeId1;
			SelectTypeId2 = selectTypeId2;
			SelectTypeId3 = selectTypeId3;
		}

		public QuestMapBuildingMB() :base(0L, false, ""){}

		public long GetTypeIdByIndex(int index)
		{
			if (index == 0)
			{
				return this.SelectTypeId1;
			}
			if (index != 0)
			{
				if (index != 1)
				{
				}
				return this.SelectTypeId3;
			}
			return this.SelectTypeId2;
		}

		public bool IsChangeable()
		{
			return true;
		}
	}
}
