using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class VipTable : TableBase<VipMB>
	{
		public VipMB GetByLevel(long vipLevel)
		{
            return _datas.FirstOrDefault(d => d.Lv == vipLevel);
		}

		public long GetMaxLevel()
		{
			// int num = 0;
			// num++;
			// long num2;
			// return num2;
			throw new NullReferenceException();
		}

		public VipTable()
		{
		}
	}
}
