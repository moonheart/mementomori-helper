using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[MessagePackObject(true)]
	public class EventMissionReward : IUserCharacterItem
	{
		[PropertyOrder(1)]
		[Nest(true, 1)]
		public UserItem EventItem
		{
			get;
			set;
		}

		[PropertyOrder(2)]
		public CharacterRarityFlags RarityFlags
		{
			get;
			set;
		}

		UserItem IUserCharacterItem.Item
		{
			get;
		}

		public EventMissionReward()
		{
		}
	}
}
