using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Mission
{
	[MessagePackObject(true)]
	[Description("パネルミッションの報酬情報")]
	public class PanelMissionRewardInfo
	{
		[Description("クリアクエストID")]
		public long ClearQuestId { get; set; }

		[Nest(true, 3)]
		[Description("報酬アイテムリスト")]
		public List<UserItem> QuestProgressItemList { get; set; }
	}
}
