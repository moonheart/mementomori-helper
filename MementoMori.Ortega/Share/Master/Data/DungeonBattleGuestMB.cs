using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("時空の洞窟 アシストキャラクター")]
	[MessagePackObject(true)]
	public class DungeonBattleGuestMB : MasterBookBase, IHasJstStartEndTime
	{
		[Description("キャラクターID")]
		[PropertyOrder(1)]
		public long CharacterId
		{
			get;
		}

		[Description("優先的に出現させるか")]
		[PropertyOrder(2)]
		public bool IsPickup
		{
			get;
		}

		[Description("優先出現終了時間(JST)")]
		[PropertyOrder(4)]
		public string EndTimeFixJST
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("優先出現開始時間(JST)")]
		public string StartTimeFixJST
		{
			get;
		}

		[SerializationConstructor]
		public DungeonBattleGuestMB(long id, bool? isIgnore, string memo, long characterId, bool isPickup, string startTimeFixJST, string endTimeFixJST)
			:base(id, isIgnore, memo)
		{
			CharacterId = characterId;
			IsPickup = isPickup;
			StartTimeFixJST = startTimeFixJST;
			EndTimeFixJST = endTimeFixJST;
		}

		public DungeonBattleGuestMB() : base(0, false, "")
		{
		}
	}
}
