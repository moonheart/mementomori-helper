using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class UserMiningQuestReinforcementInfo
	{
		[Description("強化ID")]
		public long MiningQuestReinforcementId { get; set; }

		[Description("強化Lv")]
		public int Level { get; set; }
	}
}
