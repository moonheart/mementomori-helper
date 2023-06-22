using System.Runtime.CompilerServices;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.DtoInfo
{
	[MessagePackObject(true)]
	public class UserDungeonBattleCharacterDtoInfo
	{
		public long CharacterId
		{
			get;
			set;
		}

		public int CurrentHpPerMill
		{
			get;
			set;
		}

		public long GuestId
		{
			get;
			set;
		}

		public string Guid
		{
			get;
			set;
		}

		public bool IsGuest()
		{
			return GuestId > 0;
		}

		public UserDungeonBattleCharacterDtoInfo()
		{
		}
	}
}
