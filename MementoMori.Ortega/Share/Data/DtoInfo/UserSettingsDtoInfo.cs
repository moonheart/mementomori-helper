using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
    [MessagePackObject(true)]
    public class UserSettingsDtoInfo
    {
        public PlayerSettingsType PlayerSettingsType { get; set; }

        public long Value { get; set; }

        public long PlayerId { get; set; }

        public UserSettingsDtoInfo()
        {
        }
    }
}