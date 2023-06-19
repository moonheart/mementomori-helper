using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Auth
{
    [MessagePackObject(true)]
    [Description("アプリアセットバージョン情報")]
    public class AppAssetVersionInfo
    {
        [Description("環境種別")]
        public AppAssetVersionEnvType EnvType { get; set; }

        [Description("無条件ダウンロードをスキップするか")]
        public bool IsSkipAssetDownload { get; set; }

        [Description("アプリバージョン")]
        public string Version { get; set; }

        public AppAssetVersionInfo()
        {
        }
    }
}