using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.RentalRaid
{
	[MessagePackObject(true)]
	public class ShareCharacterOwnerInfo
	{
        public string CharacterGuid { get; set; }

        public string PlayerName { get; set; }

		public long WorldId { get; set; }

		public long MainCharacterIconId { get; set; }

		public LegendLeagueClassType PrevLegendLeagueClass { get; set; }
	}
}
