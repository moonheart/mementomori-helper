using MementoMori.Ortega.Share.Data.DtoInfo;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.ApiInterface.DungeonBattle
{
    [MessagePackObject(true)]
    public class ExecRecoveryResponse : ApiResponseBase
    {
        public List<UserDungeonBattleCharacterDtoInfo> UserDungeonBattleCharacterDtoInfos { get; set; }

        public UserDungeonBattleDtoInfo UserDungeonBattleDtoInfo { get; set; }

        public ExecRecoveryResponse()
        {
        }
    }
}