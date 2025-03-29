using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.WorldGuidance
{
	[MessagePackObject(true)]
	[Description("ワールド誘導情報")]
	public class WorldGuidanceInfo
	{
		[PropertyOrder(1)]
		[Description("ワールド誘導ダイアログタイプ")]
		public WorldGuidanceType WorldGuidanceType { get; set; }

		[PropertyOrder(2)]
		[Description("誘導先ワールドID")]
		public long WorldGuidanceId { get; set; }

		public WorldGuidanceInfo(WorldGuidanceType worldGuidanceType = WorldGuidanceType.None, long worldGuidanceId = 0L)
		{
			this.WorldGuidanceType = worldGuidanceType;
			this.WorldGuidanceId = worldGuidanceId;
		}
	}
}
