using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Mission
{
	[MessagePackObject(true)]
	[Description("ミッションクリア個数の報酬情報")]
	public class MissionClearCountRewardInfo
	{
		[Description("クリアクエストID")]
		public long ClearQuestId { get; set; }

		[Nest(true, 0)]
		[Description("報酬アイテムリスト")]
		public IReadOnlyList<MissionReward> QuestProgressItemList { get; set; }
	}
}
