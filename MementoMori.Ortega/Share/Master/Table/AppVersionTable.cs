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


        public bool IsLessThan(string version)
        {
            // GameManager instance = SingletonMonoBehaviour.Instance;
            // int num = 0;
            // int deviceType = (int)instance.GetDeviceType();
            // num++;
            // bool flag = Version.TryParse(num, num);
            // int num2 = 0;
            // if (!(num == num2) && Version.TryParse(version, num))
            // {
            //     return num < num;
            // }
            throw new NullReferenceException();
        }

        public bool IsDisableGuildRecruit()
        {
            return this.IsLessThan("2.5.0");
        }

        public bool IsDisableGuildMission()
        {
            return this.IsLessThan("2.5.0");
        }

        public bool IsDisableGuildMemberDisplayOnlineStatus()
        {
            return this.IsLessThan("2.6.0");
        }
	}
}
