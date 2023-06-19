using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Auth
{
    [MessagePackObject(true)]
    [Description("メンテナンス情報")]
    public class MaintenanceInfo
    {
        [Description("対象サーバー種別")]
        public MaintenanceServerType MaintenanceServerType { get; set; }

        [Description("開始日時")]
        public DateTime StartTimeFixJST { get; set; }

        [Description("終了日時")]
        public DateTime EndTimeFixJST { get; set; }

        [Description("プラットフォームリスト")]
        public List<int> MaintenancePlatformTypes { get; set; }

        [Description("領域タイプ")]
        public MaintenanceAreaType MaintenanceAreaType { get; set; }

        [Description("領域Idタイプ")]
        public List<long> AreaIds { get; set; }

        [Description("メンテナンス対象機能種別リスト")]
        public List<int> MaintenanceFunctionTypes { get; set; }

        public MaintenanceInfo()
        {
        }
    }
}