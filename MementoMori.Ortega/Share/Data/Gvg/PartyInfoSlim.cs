using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(false)]
	public class PartyInfoSlim
	{
		[Key(0)]
		public List<long> AliveCharacterIdList { get; set; }

		[Key(1)]
		public long BattlePower { get; set; }

		[Key(2)]
		public long OwnerPlayerId { get; set; }

		[Key(3)]
		public long OwnerPlayerRank { get; set; }

		[Key(4)]
		public List<UserGvgCharacterInfoSlim> UserGvgCharacterInfoSlimList { get; set; }
	}
}
