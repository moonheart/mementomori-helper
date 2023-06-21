using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item
{
	[MessagePackObject(true)]
	public class MissionReward : IUserCharacterItem
	{
		[PropertyOrder(1)]
		[Nest(true, 1)]
		public UserItem Item
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

		public MissionReward()
		{
		}
	}
}
