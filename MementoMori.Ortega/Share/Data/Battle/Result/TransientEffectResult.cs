using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle.Result
{
	[MessagePackObject(true)]
	public class TransientEffectResult
	{
		public List<TransientEffect> TransientEffects
		{
			get;
			set;
		}

		public List<SubSkillResult> TransientEffectSubSkillResults
		{
			get;
			set;
		}

		public long RemainHp
		{
			get;
			set;
		}

		public TransientEffectResult()
		{
		}
	}
}
