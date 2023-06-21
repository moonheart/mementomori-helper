using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Data;

namespace MementoMori.Ortega.Share.Master.Table
{
	public class AppVersionTable : TableBase<AppVersionMB>
	{
		public AppVersionMB GetByDeviceType(DeviceType deviceType)
		{
			int num = 0;
			num++;
			throw new NullReferenceException();
		}

		public Version GetAppVersion(DeviceType deviceType)
		{
			// int num = 0;
			// num++;
			// bool flag = Version.TryParse(num, num);
			throw new NullReferenceException();
		}

		public bool IsLessThan(DeviceType deviceType, string version)
		{
			// int num = 0;
			// if (num < (int)deviceType)
			// {
			// 	if ("{il2cpp array field il2cppMethodInfo->}" != deviceType)
			// 	{
			// 		num++;
			// 	}
			// 	bool flag = Version.TryParse(num, num);
			// }
			// int num2 = 0;
			// if (!(num == num2) && Version.TryParse(version, num))
			// {
			// 	return num < num;
			// }
			throw new NullReferenceException();
		}

		public AppVersionTable()
		{
		}
	}
}
