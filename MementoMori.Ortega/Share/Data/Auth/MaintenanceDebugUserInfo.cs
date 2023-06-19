using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Auth
{
    [MessagePackObject(true)]
    [Description("メインテナンス突破ユーザー情報")]
    public class MaintenanceDebugUserInfo
    {
        [Description("ユーザーID")]
        public long UserId { get; set; }

        [Description("プレイヤーID")]
        public long PlayerId { get; set; }

        [Description("デバックユーザー状態")]
        public bool IsDebugUser { get; set; }

        public MaintenanceDebugUserInfo()
        {
        }
    }
}