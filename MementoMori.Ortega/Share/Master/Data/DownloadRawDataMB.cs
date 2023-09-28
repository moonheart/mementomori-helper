using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
    [MessagePackObject(true)]
    [Description("ダウンロードファイル")]
    public class DownloadRawDataMB : MasterBookBase
    {
        [PropertyOrder(1)]
        [Description("ファイルパス")]
        public string FilePath { get; }

        [PropertyOrder(2)]
        [Description("ファイルサイズ")]
        public long FileSize { get; }

        [PropertyOrder(3)]
        [Description("ダウンロードタイプ")]
        public RawDataDownloadType RawDataDownloadType { get; }

        [PropertyOrder(4)]
        [Description("Etag")]
        public string Etag { get; }

        [SerializationConstructor]
        public DownloadRawDataMB(long id, bool? isIgnore, string memo, string filePath, long fileSize, RawDataDownloadType rawDataDownloadType, string etag)
            : base(id, isIgnore, memo)
        {
            FilePath = filePath;
            FileSize = fileSize;
            RawDataDownloadType = rawDataDownloadType;
            Etag = etag;
        }

        public DownloadRawDataMB() : base(0L, null, null)
        {
        }
    }
}