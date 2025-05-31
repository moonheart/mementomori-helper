using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Chat
{
	[MessagePackObject(true)]
	public class ChatBattlePropertyInfo
	{
		public long BattleId { get; set; }

		public BattleType BattleType { get; set; }

		public TowerType TowerType { get; set; }

		public string BattleToken { get; set; }
	}
}
