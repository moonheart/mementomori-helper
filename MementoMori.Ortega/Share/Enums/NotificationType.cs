using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	[Description("通知タイプ")]
	public enum NotificationType
	{
		None,
		[Description("新しいガチャ権を入手")]
		NewGachaTicket,
		[Description("セレクトリストに登録しているキャラのレアリティが最大")]
		MaxRarityInSelectList,
		[Description("セレクトリストに登録できるキャラが追加")]
		NewCharacterInSelectList,
		[Description("無料ガチャ回数が残っている")]
		GachaFreeCount,
		[Description("ギルドレイドコンテンツ挑戦/解放可能")]
		GuildRaidAvailable,
		[Description("新しいギルド加入申請")]
		NewGuildJoinRequest,
		[Description("ギルドレベルアップ")]
		GuildLevelUp,
		[Description("Local GVG 受け取り可能なギルドバトル報酬がある場合")]
		LocalGvgReward,
		[Description("Global GVG 受け取り可能なギルドバトル報酬がある場合")]
		GlobalGvgReward
	}
}
