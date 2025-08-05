using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum RentalRaidRankingServerType
	{
		None,
		[Description("ワールドランキング")]
		World,
		[Description("サーバーランキング")]
		Server
	}
}
