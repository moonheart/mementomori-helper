using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BookSortAssistance
{
	[MessagePackObject(true)]
	public class BookSortAssistanceRewardGrade
	{
		[Description("キャラレアリティ")]
		public CharacterRarityFlags CharacterRarityFlags { get; set; }

		[Description("グレード")]
		public long Grade { get; set; }
	}
}
