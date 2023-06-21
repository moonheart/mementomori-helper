using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BountyQuest
{
	[MessagePackObject(true)]
	public class BountyQuestEventTargetQuestTypeInfo
	{
		public BountyQuestType BountyQuestType
		{
			get;
			set;
		}

		public BountyQuestEventTargetQuestTypeInfo()
		{
		}
	}
}
