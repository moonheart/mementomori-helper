using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Battle
{
	[Description("バトルパラメータ")]
	[MessagePackObject(true)]
	public class BattleParameter : IDeepCopy<BattleParameter>
	{
		[Description("攻撃力")]
		public long AttackPower
		{
			get;
			set;
		}

		[Description("回避")]
		public int Avoidance
		{
			get;
			set;
		}

		[Description("クリティカル")]
		public int Critical
		{
			get;
			set;
		}

		[Description("クリダメ強化")]
		public long CriticalDamageEnhance
		{
			get;
			set;
		}

		[Description("クリティカル耐性")]
		public int CriticalResist
		{
			get;
			set;
		}

		[Description("ダメージ強化")]
		public long DamageEnhance
		{
			get;
			set;
		}

		[Description("カウンタ\u200b")]
		public int DamageReflect
		{
			get;
			set;
		}

		[Description("弱体効果命中\u200b")]
		public int DebuffHit
		{
			get;
			set;
		}

		[Description("弱体効果耐性")]
		public int DebuffResist
		{
			get;
			set;
		}

		[Description("防御力")]
		public long Defense
		{
			get;
			set;
		}

		[Description("防御貫通力")]
		public long DefensePenetration
		{
			get;
			set;
		}

		[Description("命中")]
		public int Hit
		{
			get;
			set;
		}

		[Description("HP")]
		public long HP
		{
			get;
			set;
		}

		[Description("HP吸収")]
		public int HpDrain
		{
			get;
			set;
		}

		[Description("魔法クリダメ緩和")]
		public int MagicCriticalDamageRelax
		{
			get;
			set;
		}

		[Description("魔法ダメージ緩和")]
		public long MagicDamageRelax
		{
			get;
			set;
		}

		[Description("物理クリダメ緩和")]
		public int PhysicalCriticalDamageRelax
		{
			get;
			set;
		}

		[Description("物理ダメージ緩和")]
		public long PhysicalDamageRelax
		{
			get;
			set;
		}

		[Description("スピード\u200b")]
		public int Speed
		{
			get;
			set;
		}

		public BattleParameter DeepCopy()
		{
			BattleParameter battleParameter = new BattleParameter();
			battleParameter.HP = HP; 
			battleParameter.AttackPower = AttackPower; 
			battleParameter.PhysicalDamageRelax = PhysicalDamageRelax; 
			battleParameter.MagicDamageRelax = MagicDamageRelax; 
			battleParameter.Hit = Hit; 
			battleParameter.Avoidance = Avoidance; 
			battleParameter.Critical = Critical; 
			battleParameter.CriticalResist = CriticalResist; 
			battleParameter.CriticalDamageEnhance = CriticalDamageEnhance; 
			battleParameter.PhysicalCriticalDamageRelax = PhysicalCriticalDamageRelax; 
			battleParameter.MagicCriticalDamageRelax = MagicCriticalDamageRelax; 
			battleParameter.DefensePenetration = DefensePenetration; 
			battleParameter.Defense = Defense; 
			battleParameter.DamageEnhance = DamageEnhance; 
			battleParameter.DebuffHit = DebuffHit; 
			battleParameter.DebuffResist = DebuffResist; 
			battleParameter.DamageReflect = DamageReflect; 
			battleParameter.HpDrain = HpDrain; 
			battleParameter.Speed = Speed; 
			return battleParameter;
		}

		public long GetParameter(BattleParameterType parameterType)
		{
			switch (parameterType)
			{
				case BattleParameterType.Hp:
					return HP;
				case BattleParameterType.AttackPower:
					return AttackPower;
					break;
				case BattleParameterType.PhysicalDamageRelax:
					return PhysicalDamageRelax;
					break;
				case BattleParameterType.MagicDamageRelax:
					return MagicDamageRelax;
					break;
				case BattleParameterType.Hit:
					return Hit;
					break;
				case BattleParameterType.Avoidance:
					return Avoidance;
					break;
				case BattleParameterType.Critical:
					return Critical;
					break;
				case BattleParameterType.CriticalResist:
					return CriticalResist;
					break;
				case BattleParameterType.CriticalDamageEnhance:
					return CriticalDamageEnhance;
					break;
				case BattleParameterType.PhysicalCriticalDamageRelax:
					return PhysicalCriticalDamageRelax;
					break;
				case BattleParameterType.MagicCriticalDamageRelax:
					return MagicCriticalDamageRelax;
					break;
				case BattleParameterType.DefensePenetration:
					return DefensePenetration;
					break;
				case BattleParameterType.Defense:
					return Defense;
					break;
				case BattleParameterType.DamageEnhance:
					return DamageEnhance;
					break;
				case BattleParameterType.DebuffHit:
					return DebuffHit;
					break;
				case BattleParameterType.DebuffResist:
					return DebuffResist;
					break;
				case BattleParameterType.DamageReflect:
					return DamageReflect;
				case BattleParameterType.HpDrain:
					return HpDrain;
				case BattleParameterType.Speed:
					return Speed;
				default:
					throw new ArgumentOutOfRangeException(nameof(parameterType), parameterType, null);
			}
		}

		public BattleParameter BattleParameterCorrected(double rate)
		{
			BattleParameter battleParameter = new BattleParameter();
			battleParameter.HP = (long)(battleParameter.HP * rate);
			battleParameter.AttackPower = (long)(battleParameter.AttackPower * rate);
			battleParameter.PhysicalDamageRelax = (long)(battleParameter.PhysicalDamageRelax * rate);
			battleParameter.MagicDamageRelax = (long)(battleParameter.MagicDamageRelax * rate);
			battleParameter.Hit = (int)(battleParameter.Hit * rate);
			battleParameter.Avoidance = (int)(battleParameter.Avoidance * rate);
			battleParameter.Critical = (int)(battleParameter.Critical * rate);
			battleParameter.CriticalResist = (int)(battleParameter.CriticalResist * rate);
			battleParameter.CriticalDamageEnhance = (long)(battleParameter.CriticalDamageEnhance * rate);
			battleParameter.PhysicalCriticalDamageRelax = (int)(battleParameter.PhysicalCriticalDamageRelax * rate);
			battleParameter.MagicCriticalDamageRelax = (int)(battleParameter.MagicCriticalDamageRelax * rate);
			battleParameter.DefensePenetration = (long)(battleParameter.DefensePenetration * rate);
			battleParameter.Defense = (long)(battleParameter.Defense * rate);
			battleParameter.DamageEnhance = (long)(battleParameter.DamageEnhance * rate);
			battleParameter.DebuffHit = (int)(battleParameter.DebuffHit * rate);
			battleParameter.DebuffResist = (int)(battleParameter.DebuffResist * rate);
			battleParameter.DamageReflect = (int)(battleParameter.DamageReflect * rate);
			battleParameter.HpDrain = (int)(battleParameter.HpDrain * rate);
			battleParameter.Speed = (int)(battleParameter.Speed * rate);
			return battleParameter;
		}

		public BattleParameter Add(BattleParameter battleParameter)
		{
			BattleParameter battleParameter2 = new BattleParameter();
			battleParameter2.HP = HP + battleParameter.HP;
			battleParameter2.AttackPower = AttackPower + battleParameter.AttackPower;
			battleParameter2.PhysicalDamageRelax = PhysicalDamageRelax + battleParameter.PhysicalDamageRelax;
			battleParameter2.MagicDamageRelax = MagicDamageRelax + battleParameter.MagicDamageRelax;
			battleParameter2.Hit = Hit + battleParameter.Hit;
			battleParameter2.Avoidance = Avoidance + battleParameter.Avoidance;
			battleParameter2.Critical = Critical + battleParameter.Critical;
			battleParameter2.CriticalResist = CriticalResist + battleParameter.CriticalResist;
			battleParameter2.CriticalDamageEnhance = CriticalDamageEnhance + battleParameter.CriticalDamageEnhance;
			battleParameter2.PhysicalCriticalDamageRelax = PhysicalCriticalDamageRelax + battleParameter.PhysicalCriticalDamageRelax;
			battleParameter2.MagicCriticalDamageRelax = MagicCriticalDamageRelax + battleParameter.MagicCriticalDamageRelax;
			battleParameter2.DefensePenetration = DefensePenetration + battleParameter.DefensePenetration;
			battleParameter2.Defense = Defense + battleParameter.Defense;
			battleParameter2.DamageEnhance = DamageEnhance + battleParameter.DamageEnhance;
			battleParameter2.DebuffHit = DebuffHit + battleParameter.DebuffHit;
			battleParameter2.DebuffResist = DebuffResist + battleParameter.DebuffResist;
			battleParameter2.DamageReflect = DamageReflect + battleParameter.DamageReflect;
			battleParameter2.HpDrain = HpDrain + battleParameter.HpDrain;
			battleParameter2.Speed = Speed + battleParameter.Speed;
			return battleParameter2;
		}

		public BattleParameter()
		{
		}
	}
}
