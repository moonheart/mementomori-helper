using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
	[MessagePackObject(true)]
	public class RecitationSettingData
	{
		[PropertyOrder(1)]
		[Description("歌詞開始時間（秒）")]
		public float SongLyricsStartTime { get; set; }

		[PropertyOrder(2)]
		[Description("歌詞テキストキー")]
		public string SongLyricsKey{ get; set; }

		[PropertyOrder(3)]
		[Description("朗読テキストタイプ")]
		public RecitationTextType RecitationTextType { get; set; }
	}
}
