using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("楽曲")]
	public class MusicMB : MasterBookBase
	{
		[DateTimeString]
		[PropertyOrder(1)]
		[Description("公開日時")]
		public string StartTime { get; }

		[PropertyOrder(2)]
		[Description("並び順")]
		public int Order { get; }

		[PropertyOrder(3)]
		[Description("曲名キー")]
		public string NameKey { get; }

		[PropertyOrder(4)]
		[Description("歌手名キー")]
		public string SingerKey { get; }

		[PropertyOrder(5)]
		[Description("歌詞キー")]
		public string LyricsKey { get; }

		[PropertyOrder(6)]
		[Description("楽曲パス")]
		public string MusicPath { get; set; }

		[PropertyOrder(7)]
		[Description("サムネイルパス")]
		public string ThumbnailPath { get; set; }

		[PropertyOrder(8)]
		[Description("専用消費アイテム個数")]
		public int ConsumeMusicTicketCount { get; set; }

		[SerializationConstructor]
		public MusicMB(long id, bool? isIgnore, string memo, string startTime, int order, string nameKey, string singerKey, string lyricsKey, string musicPath, string thumbnailPath, int consumeMusicTicketCount)
			: base(id, isIgnore, memo)
		{
			StartTime = startTime;
            Order = order;
            NameKey = nameKey;
            SingerKey = singerKey;
            LyricsKey = lyricsKey;
            MusicPath = musicPath;
            ThumbnailPath = thumbnailPath;
            ConsumeMusicTicketCount = consumeMusicTicketCount;
		}

		public MusicMB():base(0L, null, null)
		{
			
		}
	}
}
