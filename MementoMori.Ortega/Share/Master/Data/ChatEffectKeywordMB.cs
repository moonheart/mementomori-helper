using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("チャット演出キーワード")]
	public class ChatEffectKeywordMB : MasterBookBase, IHasJstStartEndTime
	{
		[PropertyOrder(1)]
		[Description("キーワード")]
		public string Keyword { get; }

		[PropertyOrder(2)]
		[Description("演出ID")]
		public long EffectId { get; }

		[PropertyOrder(3)]
		[Description("開始時間（JST）")]
		public string StartTimeFixJST { get; }

		[PropertyOrder(4)]
		[Description("終了時間（JST）")]
		public string EndTimeFixJST { get; }

		[SerializationConstructor]
		public ChatEffectKeywordMB(long id, bool? isIgnore, string memo, string keyword, long effectId, string startTimeFixJst, string endTimeFixJst)
        :base(id, isIgnore, memo)
		{
            Keyword = keyword;
            EffectId = effectId;
            StartTimeFixJST = startTimeFixJst;
            EndTimeFixJST = endTimeFixJst;
		}

		public ChatEffectKeywordMB():base(0L, null, null)
		{
			
		}
	}
}
