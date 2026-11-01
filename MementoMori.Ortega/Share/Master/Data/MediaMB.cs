using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;
using System.ComponentModel;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("メディア")]
	public class MediaMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("メディアタイプ")]
		public MediaType MediaType
		{
			get;
		}

		[PropertyOrder(2)]
		[Description("バナーの表示順(昇順)")]
		public int SortOrder
		{
			get;
		}

		[PropertyOrder(3)]
		[Description("画像Id")]
		public long MovieId
		{
			get;
		}

		[PropertyOrder(4)]
		[Description("メディアタイトルキー")]
		public string MediaTitleKey
		{
			get;
		}

		[PropertyOrder(5)]
		[Description("URLキー")]
		public string UrlKey
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("ミッションId")]
		public long MissionId
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("表示される設定言語のリスト")]
		public IReadOnlyList<int> DisplayLanguageTypeList
		{
			get;
		}

		[SerializationConstructor]
		public MediaMB(long id, bool? isIgnore, string memo, MediaType mediaType, int sortOrder, long movieId, string mediaTitleKey, string urlKey, long missionId, IReadOnlyList<int> displayLanguageTypeList)
			:base(id, isIgnore, memo)
		{
			MediaType = mediaType;
			SortOrder = sortOrder;
			MovieId = movieId;
			MediaTitleKey = mediaTitleKey;
			UrlKey = urlKey;
			MissionId = missionId;
			DisplayLanguageTypeList = displayLanguageTypeList;
		}

		public MediaMB():base(0, false, "")
		{
		}
	}
}
