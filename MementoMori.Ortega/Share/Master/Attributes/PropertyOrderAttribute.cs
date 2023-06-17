namespace MementoMori.Ortega.Share.Master.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyOrderAttribute : Attribute
	{
		public int Order
		{
			get;
		}

		public PropertyOrderAttribute(int order)
		{
			this.Order = order;
		}
	}
}
