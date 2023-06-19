using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.BountyQuest
{
    [MessagePackObject(true)]
    public class BountyQuestMemberInfo
    {
        [Description("プレイヤーID")]
        public long PlayerId { get; set; }

        [Description("キャラクターID")]
        public long CharacterId { get; set; }

        [Description("ユーザーキャラクターGUID")]
        public string UserCharacterGuid { get; set; }

        [Description("キャラクターレアリティ")]
        public CharacterRarityFlags CharacterRarityFlags { get; set; }

        public BountyQuestMemberInfo()
        {
        }
    }
}