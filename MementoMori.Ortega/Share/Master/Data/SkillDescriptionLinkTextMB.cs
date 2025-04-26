using System.ComponentModel;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[MessagePackObject(true)]
	[Description("スキルのリンクテキスト")]
	public class SkillDescriptionLinkTextMB : MasterBookBase
	{
		[PropertyOrder(1)]
		[Description("リンクワード")]
		public string TargetTextKey { get; }

		[PropertyOrder(2)]
		[Description("リンク先表示テキストリスト")]
		public IReadOnlyList<string> LinkTextKeys { get; }

		[SerializationConstructor]
		public SkillDescriptionLinkTextMB(long id, bool? isIgnore, string memo, string targetTextKey, IReadOnlyList<string> linkTextKeys)
            : base(id, isIgnore, memo)
        {
            this.TargetTextKey = targetTextKey;
            this.LinkTextKeys = linkTextKeys;
        }

		public SkillDescriptionLinkTextMB()
            :base(0, null, null)
		{
			
		}
	}
}
