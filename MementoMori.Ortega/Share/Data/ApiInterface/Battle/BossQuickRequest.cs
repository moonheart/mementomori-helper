using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.Battle
{
	[MessagePackObject(true)]
	[OrtegaApi("battle/bossQuick", true, false)]
	public class BossQuickRequest : ApiRequestBase
	{
		public long QuestId { get; set; }

		public int QuickCount { get; set; }

        public bool IsAvailableBossChallengeTicket { get; set; }

        public long TargetEquipmentSetMaterialId { get; set; }
	}
}
