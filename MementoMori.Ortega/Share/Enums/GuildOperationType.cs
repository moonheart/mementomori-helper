using System.ComponentModel;

namespace MementoMori.Ortega.Share.Enums
{
	public enum GuildOperationType
	{
		[Description("ギルド名の更新")]
		UpdateGuildName = 1,
		[Description("伝令の更新")]
		UpdateAnnouncement,
		[Description("宣伝の更新")]
		UpdateDescription,
		[Description("方針の更新")]
		UpdatePolicy,
		[Description("システムメッセージ設定の更新")]
		UpdateSystemChatOption,
		[Description("加入条件の更新")]
		UpdateJoinCondition,
		[Description("サブマスターの任命、解任")]
		AppointmentSubLeader,
		[Description("指揮官の任命、解任")]
		AppointmentCommander,
		[Description("ベテランの任命、解任")]
		AppointmentVeteran,
		[Description("メンバーの除名")]
		RemoveMember,
		[Description("加入申請の管理")]
		ManagementJoinRequest,
		[Description("最終ログイン表示")]
		DisplayLastLogin,
		[Description("チャットアナウンスの管理")]
		ManagementChatAnnounce,
		[Description("ギルドバトル/グランドバトルの管理")]
		ManagementGvgBattle,
		[Description("ギルドレイドボスの解放")]
		OpenGuildRaid,
		[Description("ギルドメンバーの勧誘")]
		RecruitGuildMember,
		[Description("拠点メモの管理")]
		ManagementGvgCastleMemo,
		[Description("ギルドアンケートの管理")]
		ManagementGuildSurvey,
		[Description("他プレイヤーの作成したチャットアナウンス・アンケートの削除")]
		DeleteNotOwnChatAnnounceAndSurvey
	}
}
