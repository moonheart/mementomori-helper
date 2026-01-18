using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.RentalRaid
{
	[MessagePackObject(true)]
	public class ShareCharacterOwnerInfo : IPlayerIconInfo, IShareCharacterOwnerInfo
	{
        public string CharacterGuid { get; set; }

        public string PlayerName { get; set; }

		public long WorldId { get; set; }

		public long MainCharacterIconId { get; set; }

        public long MainCharacterIconEffectId { get; set; }

		public LegendLeagueClassType PrevLegendLeagueClass { get; set; }
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
            return PrevLegendLeagueClass;
        }

        IPlayerIconInfo IShareCharacterOwnerInfo.PlayerIconInfo => this;
    }
}
