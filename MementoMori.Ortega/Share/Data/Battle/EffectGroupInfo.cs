using System.ComponentModel;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
	[MessagePackObject(true)]
	[Description("エフェクトグループ情報")]
	public class EffectGroupInfo
	{
		[Description("エフェクトグループID")]
		public long EffectGroupId { get; set; }

		[Description("優先度")]
		public int OrderNumber { get; set; }
	}
}
