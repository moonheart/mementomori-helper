using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Character;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(true)]
	public class UserGvgCharacterInfo
	{
		public long BattlePower { get; set; }

		public bool IsSettingLevelLink { get; set; }

		public UserCharacterInfo UserCharacterInfo { get; set; }
	}
}
