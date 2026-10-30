using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Gvg
{
	[MessagePackObject(true)]
	public class GvgActivePlayerInfo
	{
		public int Rank { get; set; }

		public long PlayerId { get; set; }

		public long GuildId { get; set; }

		public BattleFieldCharacterGroupType BattleFieldCharacterGroupType { get; set; }

		public long MaxPartyKnockOutCount { get; set; }
	}
}
