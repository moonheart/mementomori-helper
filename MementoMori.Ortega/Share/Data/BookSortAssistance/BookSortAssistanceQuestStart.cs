using System.ComponentModel;
using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAssistanceQuestStart
	{
		[Description("お手伝い派遣クエストGUID")]
		public string UserBookSortAssistanceQuestGuid { get; set; }

		[Description("派遣キャラクターGUID")]
		public string UserCharacterGuid { get; set; }
	}
}
