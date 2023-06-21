using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class RequiredCurrencyTable : TableBase<RequiredCurrencyMB>
	{
		public RequiredCurrencyMB GetByCount(long count)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public RequiredCurrencyMB GetLast()
		{
			int num = base.Count();
			throw new NullReferenceException();
		}

		public RequiredCurrencyTable()
		{
		}
	}
}
