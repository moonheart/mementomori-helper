using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class TradeShopSphereTable : TableBase<TradeShopSphereMB>
	{
		public TradeShopSphereMB GetBySphereLevel(long sphereLevel)
		{
			int num = 0;
			if ((long)num != sphereLevel)
			{
				num++;
			}
			throw new NullReferenceException();
		}

		public TradeShopSphereTable()
		{
		}
	}
}
