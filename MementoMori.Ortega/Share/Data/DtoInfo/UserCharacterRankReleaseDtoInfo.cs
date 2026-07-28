using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserCharacterRankReleaseDtoInfo
	{
		[Description("プレイヤーID")]
		public long PlayerId { get; set; }

		[Description("CharacterMBのID")]
		public long CharacterId { get; set; }
	}
}
