using MementoMori.Ortega.Share.Data.DtoInfo;
using MementoMori.Ortega.Share.Data.DungeonBattle;
using MementoMori.Ortega.Share.Data.Item;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [MessagePackObject(true)]
    public class GetDungeonBattleInfoResponse : ApiResponseBase, IUserSyncApiResponse
    {
        public DungeonBattleLayer CurrentDungeonBattleLayer { get; set; }

        public long CurrentTermId { get; set; }

        public long EventItemId { get; set; }

        public ItemType EventItemType { get; set; }

        public long EventNo { get; set; }

        public long EventTutorialId { get; set; }

        public Dictionary<string, long> GridBattlePowerDict { get; set; }

        public List<long> RewardRelicIds { get; set; }

        public List<UserDungeonBattleCharacterDtoInfo> UserDungeonBattleCharacterDtoInfos { get; set; }

        public List<UserDungeonBattleGuestCharacterDtoInfo> UserDungeonBattleGuestCharacterDtoInfos { get; set; }

        public int UserDungeonBattleMissedCount { get; set; }

        public List<UserDungeonBattleShopDtoInfo> UserDungeonBattleShopDtoInfos { get; set; }

        public UserDungeonBattleDtoInfo UserDungeonDtoInfo { get; set; }

        public List<UserItem> SkipRewardList { get; set; }

        public long UtcEndTimeStamp { get; set; }

        public UserSyncData UserSyncData { get; set; }

        public UserDungeonBattleGuestCharacterDtoInfo GetUserDungeonBattleGuestCharacterDtoInfo(string guid)
        {
            return UserDungeonBattleGuestCharacterDtoInfos.FirstOrDefault(d => d.Guid == guid);
        }

        public long GetBattlePower(string guid)
        {
            return GridBattlePowerDict.TryGetValue(guid, out var n) ? n : 0;
        }

        public bool IsEvent()
        {
            return EventItemId > 0;
        }

        public GetDungeonBattleInfoResponse()
        {
        }
    }
}