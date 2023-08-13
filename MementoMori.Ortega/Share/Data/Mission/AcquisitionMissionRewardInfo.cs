using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Mission
{
	[MessagePackObject(true)]
	public class AcquisitionMissionRewardInfo
	{
		public List<UserCharacterDtoInfo> CharacterList { get; set; }

		public List<UserItem> ItemList{ get; set; }
	}
}
