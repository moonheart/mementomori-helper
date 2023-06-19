using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Auth
{
    [Description("新規ユーザー管理情報")]
    [MessagePackObject(true)]
    public class ManagementNewUserInfo
    {
        [Description("終了日時")]
        public DateTime EndTimeFixJST { get; set; }

        [Description("新規ユーザーの作成禁止するか")]
        public bool IsUnableToCreateUser { get; set; }

        [Description("新規ユーザー管理種別")]
        public ManagementNewUserType ManagementNewUserType { get; set; }

        [Description("開始日時")]
        public DateTime StartTimeFixJST { get; set; }

        [Description("ターゲットIdリスト")]
        public List<long> TargetIds { get; set; }

        public bool Exist(long id)
        {
            return TargetIds.Contains(id);
        }

        public ManagementNewUserInfo()
        {
        }
    }
}