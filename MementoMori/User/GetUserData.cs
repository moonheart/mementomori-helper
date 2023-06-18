using MementoMori.Ortega.Share.Data;
using MessagePack;

namespace MementoMori.User;

public class GetUserData
{
    public class Req
    {
    }

    public class Resp
    {
        public bool IsNotClearDungeonBattleMap { get; set; }
        public int[] GachaSelectListCharacterIds { get; set; }
        public UserSyncData UserSyncData { get; set; }
    }
}