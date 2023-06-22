using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserDungeonBattleEnemyDtoInfo
    {
        public string EnemyDataJson { get; set; }

        public long EnemyGuid { get; set; }

        public long GroupId { get; set; }

        public bool IsNpc { get; set; }

        public string NpcEnemyDataJson { get; set; }

        public UserDungeonBattleEnemyDtoInfo()
        {
        }
    }
}