using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BookSortAssistance
{
	[MessagePackObject(true)]
	public class UserBookSortAssistanceQuest
	{
		[Description("GUID")]
		public string Guid { get; set; }

		[Description("グレード")]
		public long Grade { get; set; }

		[Description("クエスト表示名")]
		public string NameKey { get; set; }

		[Description("状態")]
		public BookSortAssistanceQuestStatusType BookSortAssistanceQuestStatusType { get; set; }

		[Description("報酬")]
		public UserItem RewardItem { get; set; }

		[Description("派遣完了時間")]
		public long DispatchEndLocalTimeStamp { get; set; }

		[Description("派遣キャラクターID")]
		public long CharacterId { get; set; }

		[Description("派遣キャラクターレアリティ")]
		public CharacterRarityFlags CharacterRarityFlags { get; set; }
	}
}
