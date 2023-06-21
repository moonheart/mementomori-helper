using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ミッションタブ名")]
	[MessagePackObject(true)]
	public class MissionTabNameMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("ミッション種別")]
		public MissionType MissionType
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("ミッションタブ名")]
		public string TabNameKey
		{
			get;
		}

		[SerializationConstructor]
		public MissionTabNameMB(long id, bool? isIgnore, string memo, MissionType missionType, string tabNameKey)
			:base(id, isIgnore, memo)
		{
			MissionType = missionType;
			TabNameKey = tabNameKey;
		}

		public MissionTabNameMB() :base(0L, false, ""){}
	}
}
