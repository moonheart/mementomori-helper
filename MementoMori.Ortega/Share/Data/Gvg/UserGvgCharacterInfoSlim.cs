using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(false)]
	public class UserGvgCharacterInfoSlim
	{
		[Key(0)]
		public long BattlePower { get; set; }

		[Key(1)]
		public long CharacterId { get; set; }

		[Key(2)]
		public bool IsSettingLevelLink { get; set; }

		[Key(3)]
		public long Level { get; set; }

		[Key(4)]
		public CharacterRarityFlags RarityFlags { get; set; }
	}
}
