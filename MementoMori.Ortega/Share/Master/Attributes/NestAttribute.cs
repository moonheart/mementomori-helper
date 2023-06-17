namespace MementoMori.Ortega.Share.Master.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NestAttribute : Attribute
    {
        public bool ExistDeeperNest { get; }

        public int Hierarchy { get; }

        public NestAttribute(bool existDeeperNest = false, int hierarchy = 0)
        {
            this.ExistDeeperNest = existDeeperNest;
            this.Hierarchy = hierarchy;
        }
    }
}