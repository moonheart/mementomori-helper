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
			return GetBattleParameterValueText(
				battleParameterChangeInfo.BattleParameterType,
				battleParameterChangeInfo.ChangeParameterType,
				battleParameterChangeInfo.Value);
		}

		public static string GetBattleParameterValueText(BattleParameterType battleParameterType, ChangeParameterType changeParameterType, double value)
		{
			if (!IsPercentFormat(battleParameterType))
			{
				return GetBaseParameterValueText(changeParameterType, value);
			}

			return Masters.TextResourceTable.Get("[BattleParameterPercentFormat]", value * 0.01).FixSeparatorsForLocator();
		}

		public static string GetBattleParameterValueText(BattleParameterType battleParameterType, long value)
		{
			if (!IsPercentFormat(battleParameterType))
			{
				return value.ToStringWithComma();
			}

			return Masters.TextResourceTable.Get("[BattleParameterPercentFormat]", (double)(int)value * 0.01).FixSeparatorsForLocator();
		}

		private static bool IsPercentFormat(BattleParameterChangeInfo battleParameterChangeInfo)
		{
			if (battleParameterChangeInfo.ChangeParameterType == ChangeParameterType.AdditionPercent)
			{
				return true;
			}

			return IsPercentFormat(battleParameterChangeInfo.BattleParameterType);
		}

		private static bool IsPercentFormat(BattleParameterType battleParameterType)
		{
			return battleParameterType is >= BattleParameterType.CriticalDamageEnhance and <= BattleParameterType.MagicCriticalDamageRelax
				|| battleParameterType is >= BattleParameterType.DamageReflect and <= BattleParameterType.HpDrain;
		}
	}
}
