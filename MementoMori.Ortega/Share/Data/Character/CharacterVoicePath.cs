using System.ComponentModel;
using MementoMori.Ortega.Common.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
	[MessagePackObject(true)]
	public class CharacterVoicePath
	{
		[Description("TimelineId")]
		[PropertyOrder(2)]
		public int TimelineId
		{
			get;
			set;
		}

		[Description("TimelineType")]
		[PropertyOrder(1)]
		public TimelineType TimelineType
		{
			get;
			set;
		}

		public CharacterVoicePath()
		{
		}
	}
}
