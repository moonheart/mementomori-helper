using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Response
{
	[MessagePackObject(false)]
	public class GuildTowerInfoResponse
	{
		[Key(0)]
		public long BattlePlayerId { get; set; }

		[Key(1)]
		public int GuildTowerFloor { get; set; }

		[Key(2)]
		public bool IsNextFloor { get; set; }

		[Key(3)]
		public int ClearGaugeProgress { get; set; }

		[Key(4)]
		public int ComboCount { get; set; }

		[Key(5)]
		public long EndComboLocalTimeStamp { get; set; }
	}
}
