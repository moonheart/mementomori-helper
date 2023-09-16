using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class SphereTable : TableBase<SphereMB>
	{
		public SphereMB GetByCategoryId(long sphereCategoryId)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public SphereMB GetByCategoryIdAndLevel(long sphereCategoryId, long level)
        {
            return _datas.First(d => d.CategoryId == sphereCategoryId && d.Lv == level);
        }

		public List<SphereMB> GetByCategoryIds(List<long> sphereCategoryIds)
		{
			// List<SphereMB> list;
			// int num;
			// ulong num2;
			// do
			// {
			// 	list = new List();
			// 	num = 0;
			// 	bool flag;
			// 	if (flag)
			// 	{
			// 		list.Add(num);
			// 	}
			// }
			// while (num2 != (ulong)0L);
			// num++;
			// return list;
			throw new NullReferenceException();
		}

		public List<SphereMB> GetListByLevel(long level)
        {
            return _datas.Where(d => d.Lv == 1).ToList();
        }

		public SphereTable()
		{
		}
	}
}
