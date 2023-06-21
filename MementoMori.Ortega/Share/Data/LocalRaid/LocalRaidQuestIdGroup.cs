using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.LocalRaid
{
	[MessagePackObject(true)]
	public class LocalRaidQuestIdGroup
	{
		[Description("幻影の神殿のクエストグループ種別")]
		[PropertyOrder(1)]
		public LocalRaidQuestGroupType LocalRaidQuestGroupType
		{
			get;
			set;
		}

		[Nest(true, 1)]
		[Description("幻影の神殿のクエストIdグループ")]
		[PropertyOrder(2)]
		public List<LocalRaidQuestIdWeight> LocalRaidQuestIdWeights
		{
			get;
			set;
		}

		public LocalRaidQuestIdGroup()
		{
		}
	}
}
