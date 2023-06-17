using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserBoxSizeDtoInfo
    {
        [Description("CharacterBoxSizeMBを参照")]
        public long CharacterBoxSizeId { get; set; }

        [Description("プレイヤーID")]
        public long PlayerId { get; set; }

        public UserBoxSizeDtoInfo()
        {
        }
    }
}