using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserFriendBattleOptionDtoInfo
	{
		public long PlayerId { get; set; }

		public bool IsUsedLockEquipmentInDefenseParty { get; set; }

		public bool IsUsedLockEquipmentInOffenseParty { get; set; }

		public bool IsUsedBattleLeagueDeckInDefenseParty { get; set; }

		public bool IsAllowedBattle { get; set; }
        
        public string DefenseDeckComment { get; set; }
	}
}
