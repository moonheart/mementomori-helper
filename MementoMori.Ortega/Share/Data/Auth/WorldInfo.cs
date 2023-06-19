using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Auth
{
    [MessagePackObject(true)]
    [Description("ワールド情報")]
    public class WorldInfo
    {
        [Description("ゲームサーバー")]
        public long GameServerId { get; set; }

        [Description("ワールドId")]
        public long Id { get; set; }

        [Description("ワールド設立日")]
        public DateTime StartTime { get; set; }

        public WorldInfo()
        {
        }
    }
}