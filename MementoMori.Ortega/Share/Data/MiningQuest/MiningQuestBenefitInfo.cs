using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.MiningQuest
{
	[MessagePackObject(true)]
	public class MiningQuestBenefitInfo
	{
		[Description("特典レベル")]
		public int Level { get; set; }

		[Description("解放ダイヤ数")]
		public int RequiredPurchaseCount { get; set; }

		[Description("解放済みフラグ")]
		public bool IsUnlocked { get; set; }
	}
}
