using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserRankUpPrioritySettingDtoInfo
	{
		public long PlayerId { get; set; }

		public bool Enabled { get; set; }

		public Dictionary<ElementType, List<long>> SettingDict { get; set; }
	}
}
