using System.Runtime.CompilerServices;
using MementoMori.Ortega.Share.Data.Item;
using MessagePack;

namespace MementoMori.Ortega.Share.Data
{
	[MessagePackObject(true)]
	public class UnlockGridCellResult
	{
		public UnlockedGridCellInfo UnlockedGridCellInfo { get; set; }

		public List<UserItem> RewardUserItemList { get; set; }
	}
}
