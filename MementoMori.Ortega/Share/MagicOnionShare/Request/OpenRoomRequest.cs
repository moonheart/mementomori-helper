using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.MagicOnionShare.Request
{
	[MessagePackObject(false)]
	public class OpenRoomRequest
    {
        [Key(0)]
        public LocalRaidRoomConditionsType ConditionsType { get; set; }

        [Key(1)]
        public int Password { get; set; }

        [Key(2)]
        public long QuestId { get; set; }

        [Key(3)]
        public long RequiredBattlePower { get; set; }

        [Key(4)]
        public bool IsAutoStart { get; set; }
	}
}
