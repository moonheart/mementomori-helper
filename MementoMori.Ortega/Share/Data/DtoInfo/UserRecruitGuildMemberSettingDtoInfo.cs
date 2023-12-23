using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserRecruitGuildMemberSettingDtoInfo
	{
		public long GuildPowerLowerLimit { get; set; }

		public long GuildLvLowerLimit { get; set; }

		public bool IsRecruit { get; set; }
	}
}
