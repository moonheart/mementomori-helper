using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("幻影の神殿バナー")]
	public class LocalRaidBannerMB : MasterBookBase
	{
		[Description("クエスト名キー")]
		[PropertyOrder(1)]
		public string NameKey
		{
			get;
		}

		[Description("デコレーションID")]
		[PropertyOrder(2)]
		public long DecorationId
		{
			get;
		}

		[Description("デコレーション色")]
		[PropertyOrder(3)]
		public string DecorationColor
		{
			get;
		}

		[SerializationConstructor]
		public LocalRaidBannerMB(long id, bool? isIgnore, string memo, string nameKey, long decorationId, string decorationColor)
			:base(id, isIgnore, memo)
		{
			NameKey = nameKey;
			DecorationId = decorationId;
			DecorationColor = decorationColor;
		}

		public LocalRaidBannerMB() :base(0L, false, ""){}
	}
}
