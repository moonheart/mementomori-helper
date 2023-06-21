namespace MementoMori.Ortega.Share.Master
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MasterBookIdsAttribute : Attribute
    {
        public bool IsAllowDuplicate { get; }

        public Type Type { get; }

        public MasterBookIdsAttribute(Type type, bool isAllowDuplicate)
        {
            this.Type = type;
            this.IsAllowDuplicate = isAllowDuplicate;
        }
    }
}