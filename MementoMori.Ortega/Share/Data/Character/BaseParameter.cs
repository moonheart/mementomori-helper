using System.ComponentModel;
using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Character
{
    [Description("基礎パラメータ")]
    [MessagePackObject(true)]
    public class BaseParameter : IEquatable<BaseParameter>, IDeepCopy<BaseParameter>
    {
        [Description("技力\u200b")]
        public long Energy { get; set; }

        [Description("耐久力\u200b")]
        public long Health { get; set; }

        [Description("魔力")]
        public long Intelligence { get; set; }

        [Description("筋力")]
        public long Muscle { get; set; }

        public BaseParameter DeepCopy()
        {
            BaseParameter baseParameter = new BaseParameter();
            baseParameter.Health = Health;
            baseParameter.Intelligence = Intelligence;
            baseParameter.Muscle = Muscle;
            baseParameter.Energy = Energy;
            return baseParameter;
        }

        public bool Equals(BaseParameter? other)
        {
            if (other == null)
            {
                return false;
            }

            if (this == other)
            {
                return true;
            }

            if (Muscle != other.Muscle)
            {
                return false;
            }

            if (Energy != other.Energy)
            {
                return false;
            }

            if (Intelligence == other.Intelligence)
            {
                return Health == other.Health;
            }

            return false;
        }

        public BaseParameter Add(BaseParameter baseParameter)
        {
            BaseParameter baseParameter2 = new BaseParameter();
            baseParameter2.Muscle = Muscle + baseParameter.Muscle;
            baseParameter2.Energy = Energy + baseParameter.Energy;
            baseParameter2.Intelligence = Intelligence + baseParameter.Intelligence;
            baseParameter2.Health = Health + baseParameter.Health;
            return baseParameter2;
        }

        public override bool Equals(object obj)
        {
            Type type = obj.GetType();
            Type type2 = GetType();
            bool flag = type != type2;
            if (!flag && this.Muscle == (flag ? 1L : 0L) && this.Energy == (flag ? 1L : 0L) &&
                this.Intelligence == (flag ? 1L : 0L))
            {
                return this.Health == (flag ? 1L : 0L);
            }

            return false;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public long GetValue(BaseParameterType type)
        {
            switch (type)
            {
                case BaseParameterType.Muscle:
                    return Muscle;
                case BaseParameterType.Energy:
                    return Energy;
                case BaseParameterType.Intelligence:
                    return Intelligence;
                case BaseParameterType.Health:
                    return Health;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void SetValue(BaseParameterType type, long value)
        {
            switch (type)
            {
                case BaseParameterType.Muscle:
                    Muscle = value;
                    break;
                case BaseParameterType.Energy:
                    Energy = value;
                    break;
                case BaseParameterType.Intelligence:
                    Intelligence = value;
                    break;
                case BaseParameterType.Health:
                    Health = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public long GetTotal()
        {
            return Muscle + Energy + Intelligence + Health;
        }

        public BaseParameter()
        {
        }
    }
}