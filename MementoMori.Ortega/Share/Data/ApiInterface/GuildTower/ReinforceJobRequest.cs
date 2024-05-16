using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.GuildTower
{
	[MessagePackObject(true)]
	[OrtegaApi("guildTower/reinforceJob", true, false)]
	public class ReinforceJobRequest : ApiRequestBase
	{
		public JobFlags JobFlags { get; set; }

		public int Level { get; set; }

		public List<UserItem> MaterialItemList { get; set; }
	}
}
