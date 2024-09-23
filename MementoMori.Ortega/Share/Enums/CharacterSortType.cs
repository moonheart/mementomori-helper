using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("キャラクターソート種別")]
	public enum CharacterSortType
	{
		[Description("レアリティ")]
		Rarity,
		[Description("戦力")]
		BattlePower,
		[Description("アバターなしのキャラID")]
		RootCharacterId,
		[Description("キャラタイプ")]
		CharacterType,
		[Description("属性")]
		CharacterAttribute,
		[Description("キャラレベル")]
		CharacterLevel,
		[Description("レベルリンクレベル確認ダイアログ用")]
		CheckLevelLinkLevel,
		[Description("キャラ一覧画面 デフォルト設定")]
		CharacterListDefault,
		[Description("お気に入りキャラ")]
		Favorite,
		[Description("ソートしない")]
		None,
		[Description("人気投票")]
		PopularityVote
	}
}
