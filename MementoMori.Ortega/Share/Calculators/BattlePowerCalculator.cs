using MementoMori.Ortega.Share.Data.Battle;
using MementoMori.Ortega.Share.Data.Character;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Calculators
{
	public static class BattlePowerCalculator
	{
		public static long CalculateBattlePower(BattleParameter battleParameter, BaseParameter baseParameter)
		{
			return (long) Math.Truncate(
				battleParameter.HP * GetPowerCoefficient(BattleParameterType.Hp, baseParameter)
				+ battleParameter.AttackPower * GetPowerCoefficient(BattleParameterType.AttackPower, baseParameter)
				+ battleParameter.Defense * GetPowerCoefficient(BattleParameterType.Defense, baseParameter)
				+ battleParameter.PhysicalDamageRelax * GetPowerCoefficient(BattleParameterType.PhysicalDamageRelax, baseParameter)
				+ battleParameter.MagicDamageRelax * GetPowerCoefficient(BattleParameterType.MagicDamageRelax, baseParameter)
				+ battleParameter.DamageEnhance * GetPowerCoefficient(BattleParameterType.DamageEnhance, baseParameter)
				+ battleParameter.DefensePenetration * GetPowerCoefficient(BattleParameterType.DefensePenetration, baseParameter)
				+ battleParameter.Hit * GetPowerCoefficient(BattleParameterType.Hit, baseParameter)
				+ battleParameter.Avoidance * GetPowerCoefficient(BattleParameterType.Avoidance, baseParameter)
				+ battleParameter.Critical * GetPowerCoefficient(BattleParameterType.Critical, baseParameter)
				+ battleParameter.CriticalResist * GetPowerCoefficient(BattleParameterType.CriticalResist, baseParameter)
				+ battleParameter.HpDrain * 0.01 * GetPowerCoefficient(BattleParameterType.HpDrain, baseParameter)
				+ battleParameter.DamageReflect * 0.01 * GetPowerCoefficient(BattleParameterType.DamageReflect, baseParameter)
				+ battleParameter.CriticalDamageEnhance * 0.01 * GetPowerCoefficient(BattleParameterType.CriticalDamageEnhance, baseParameter)
				+ battleParameter.PhysicalCriticalDamageRelax * 0.01 * GetPowerCoefficient(BattleParameterType.PhysicalCriticalDamageRelax, baseParameter)
				+ battleParameter.MagicCriticalDamageRelax * 0.01 * GetPowerCoefficient(BattleParameterType.MagicCriticalDamageRelax, baseParameter)
				+ battleParameter.DebuffHit * GetPowerCoefficient(BattleParameterType.DebuffHit, baseParameter)
				+ battleParameter.DebuffResist * GetPowerCoefficient(BattleParameterType.DebuffResist, baseParameter)
				+ battleParameter.Speed * GetPowerCoefficient(BattleParameterType.Speed, baseParameter)
			);
		}

		public static double GetPowerCoefficient(BattleParameterType battleParameterType, BaseParameter baseParameter)
		{
			double result = battleParameterType switch
			{
				BattleParameterType.Hp => 0.05,
				BattleParameterType.AttackPower => 2.0,
				BattleParameterType.PhysicalDamageRelax => 1.5,
				BattleParameterType.MagicDamageRelax => 1.5,
				BattleParameterType.Hit => 1.0,
				BattleParameterType.Avoidance => 1.0,
				BattleParameterType.DebuffHit => 1.0,
				BattleParameterType.DebuffResist => 1.0,
				BattleParameterType.Critical => 3.0,
				BattleParameterType.CriticalResist => 3.0,
				BattleParameterType.CriticalDamageEnhance => 2000.0,
				BattleParameterType.PhysicalCriticalDamageRelax => 2000.0,
				BattleParameterType.MagicCriticalDamageRelax => 2000.0,
				BattleParameterType.DefensePenetration => 7.0,
				BattleParameterType.DamageEnhance => 7.0,
				BattleParameterType.Defense => 2.33,
				BattleParameterType.DamageReflect => 1500.0,
				BattleParameterType.HpDrain => 1500.0,
				BattleParameterType.Speed => (baseParameter.Energy + baseParameter.Health + baseParameter.Intelligence + baseParameter.Muscle) * 0.000125,
				_ => 0.0
			};

			return result;
		}
	}
}
