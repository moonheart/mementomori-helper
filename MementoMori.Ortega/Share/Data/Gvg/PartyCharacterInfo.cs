using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(false)]
	public class PartyCharacterInfo
	{
		[Key(1)]
		public int CurrentActionPoint { get; set; }

		[Key(2)]
		public bool IsDeployed { get; set; }

		[Key(0)]
		public UserGvgCharacterInfo UserGvgCharacterInfo { get; set; }
	}
}
