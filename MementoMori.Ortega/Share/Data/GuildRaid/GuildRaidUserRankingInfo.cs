using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.GuildRaid
{
	[MessagePackObject(true)]
	public class GuildRaidUserRankingInfo : IPlayerIconInfo
	{
		public LegendLeagueClassType LegendLeagueClass { get; set; }

		public long MainCharacterIconId { get; set; }

        public long MainCharacterIconEffectId { get; set; }

		public long PlayerId { get; set; }

		public string PlayerName{ get; set; }

		public long TotalDamage { get; set; }

		public float TotalDamagePercent { get; set; }
        long IPlayerIconInfo.GetIconId()
        {
            return MainCharacterIconId;
        }

        long IPlayerIconInfo.GetIconEffectId()
        {
            return MainCharacterIconEffectId;
        }

        LegendLeagueClassType IPlayerIconInfo.GetLegendLeagueClass()
        {
            return LegendLeagueClass;
        }
    }
}
