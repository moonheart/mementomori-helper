using MementoMori.Ortega.Share;
using MementoMori.Ortega.Share.Data.Equipment;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Extensions;
using MementoMori.Ortega.Share.Master.Table;

namespace MementoMori.Ortega.Common.Utils
{
	public static class ParameterUtil
	{
		public static string GetBaseParameterValueText(ChangeParameterType changeParameterType, double value)
		{
			switch (changeParameterType)
			{
				case ChangeParameterType.Addition:
					return value.ToStringWithComma();
				case ChangeParameterType.AdditionPercent:
					value *= 0.01;
					return Masters.TextResourceTable.Get("[BattleParameterPercentFormat]", value).FixSeparatorsForLocator();
				case ChangeParameterType.CharacterLevelConstantMultiplicationAddition:
					return Masters.TextResourceTable.Get("[BattleParameterCharacterLevelConstantMultiplicationAddition]", value);
				default:
					return value.ToString();
			}
		}

        public static (string key, string value) GetBaseOrBattleParameterChangeText(BattleParameterChangeInfo battleParameterChangeInfo, BaseParameterChangeInfo baseParameterChangeInfo)
        {
            if (battleParameterChangeInfo != null)
            {
                return (Masters.TextResourceTable.Get(battleParameterChangeInfo.BattleParameterType), GetBaseParameterValueText(battleParameterChangeInfo.ChangeParameterType, (long) battleParameterChangeInfo.Value));
            }

            if (baseParameterChangeInfo != null)
            {
                return (Masters.TextResourceTable.Get(baseParameterChangeInfo.BaseParameterType), GetBaseParameterValueText(baseParameterChangeInfo.ChangeParameterType, (long) baseParameterChangeInfo.Value));
            }
            return (null, null);
        }

		public static string GetBaseParameterValueText(BaseParameterChangeInfo baseParameterChangeInfo)
		{
			return GetBaseParameterValueText(baseParameterChangeInfo.ChangeParameterType, baseParameterChangeInfo.Value);
		}

		public static string GetBattleParameterValueText(BattleParameterChangeInfo battleParameterChangeInfo)
		{
			// if (battleParameterChangeInfo.BattleParameterType )
			// {
			// 	
			// }
			//
			// TextResourceTable TextResourceTable;
			// object[] array;
			// for (;;)
			// {
			// 	ChangeParameterType ChangeParameterType = battleParameterChangeInfo.ChangeParameterType;
			// 	if ("[BattleParameterPercentFormat]" > (ulong)2L && "[BattleParameterPercentFormat]" > (ulong)1L)
			// 	{
			// 		break;
			// 	}
			// 	TextResourceTable = Masters.TextResourceTable;
			// 	array = new object[1];
			// 	if (array == 0 || array != 0)
			// 	{
			// 		goto IL_37;
			// 	}
			// }
			// string text;
			// return text;
			// IL_37:
			// array[0] = array;
			// string text2 = TextResourceTable.Get("[BattleParameterPercentFormat]", array).FixSeparatorsForLocator();
			// throw new NullReferenceException();
			throw new NotImplementedException();
		}

		public static string GetBattleParameterValueText(BattleParameterType battleParameterType, ChangeParameterType changeParameterType, double value)
		{
			
			// while ("[Ba@"ttleParameterPercentFormat]"" <= (ulong)2L || ""[BattleParameterPercentFormat]"" <= (ulong)1L)
			// {
			// 	TextResourceTable TextResourceTable = Masters.TextResourceTable;
			// 	object[] array = new object[1];
			// 	if (array == 0 || array != 0)
			// 	{
			// 		array[0] = array;
			// 		return TextResourceTable.Get(""[BattleParameterPercentFormat]"", array).FixSeparatorsForLocator();
			// 	}
			// }
			// string text;
			// return text;"
			throw new NotImplementedException();

		}

		public static string GetBattleParameterValueText(BattleParameterType battleParameterType, long value)
		{
			// while ("[BattleParameterPercentFormat]" <= (ulong)2L || "[BattleParameterPercentFormat]" <= (ulong)1L)
			// {
			// 	TextResourceTable TextResourceTable = Masters.TextResourceTable;
			// 	object[] array = new object[1];
			// 	if (array == 0 || array != 0)
			// 	{
			// 		array[0] = array;
			// 		return TextResourceTable.Get("[BattleParameterPercentFormat]", array).FixSeparatorsForLocator();
			// 	}
			// }
			// return value.ToStringWithComma();
			throw new NotImplementedException();

		}

		private static bool IsPercentFormat(BattleParameterChangeInfo battleParameterChangeInfo)
		{
			if (battleParameterChangeInfo.ChangeParameterType != ChangeParameterType.AdditionPercent)
			{
			}
			return true;
		}

		private static bool IsPercentFormat(BattleParameterType battleParameterType)
		{
			return true;
		}
	}
}
