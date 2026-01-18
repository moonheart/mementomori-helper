using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAddAssistanceCondition
	{
		[Description("対象キャラID")]
		public long CharacterId { get; set; }

		[Description("対象レアリティ")]
		public CharacterRarityFlags CharacterRarityFlags { get; set; }

		[Description("対象ガチャID")]
		public long GachaCaseId { get; set; }
	}
}
