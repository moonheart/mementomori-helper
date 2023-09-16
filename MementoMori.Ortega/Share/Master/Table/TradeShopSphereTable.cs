using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class TradeShopSphereTable : TableBase<TradeShopSphereMB>
	{
		public TradeShopSphereMB GetBySphereLevel(long sphereLevel)
        {
            return _datas.First(d => d.SphereLevel == sphereLevel);
        }

		public TradeShopSphereTable()
		{
		}
	}
}
