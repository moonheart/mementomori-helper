using System.ComponentModel;
using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Equipment
{
	[Description("バトルパラメータ変動情報")]
	[MessagePackObject(true)]
	public class BattleParameterChangeInfo
	{
		[PropertyOrder(1)]
		[Description("変動するバトルパラメータ")]
		public BattleParameterType BattleParameterType
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		[Description("パラメータ増減タイプ")]
		public ChangeParameterType ChangeParameterType
		{
			get;
			set;
		}

		[Description("値")]
		[PropertyOrder(3)]
		public double Value
		{
			get;
			set;
		}

		public BattleParameterChangeInfo()
		{
		}
	}
}
