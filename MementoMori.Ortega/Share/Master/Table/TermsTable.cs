using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class TermsTable : TableBase<TermsMB>
	{
		public TermsMB GetByTimeServerIdAndDeviceType(long timeServerId, DeviceType deviceType)
		{
			int num = 0;
			if (deviceType != DeviceType.DmmGames)
			{
				num++;
			}
			num++;
			throw new NullReferenceException();
		}

		public TermsTable()
		{
		}
	}
}
