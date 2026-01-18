using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Interface
{
	public interface IPlayerIconInfo
	{
		long GetIconId();

		long GetIconEffectId();

		LegendLeagueClassType GetLegendLeagueClass();
	}
}
