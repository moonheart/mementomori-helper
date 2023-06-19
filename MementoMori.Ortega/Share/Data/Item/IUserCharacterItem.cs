using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.Item
{
	[Description("キャラクターレアリティを持つ可能性があるアイテムが実装するインターフェース")]
	public interface IUserCharacterItem
	{
		UserItem Item
		{
			get;
		}

		CharacterRarityFlags RarityFlags
		{
			get;
		}
	}
}
