using System.ComponentModel;
using MessagePack;
using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Data.UnionMission
{
	[MessagePackObject(true)]
	public class UnionMissionPanelInfo
	{
		[Description("パネルを一意に識別するID")]
		public long MissionPanelId { get; set; }

		[Description("ミッションID")]
		public long MissionId { get; set; }

		[Description("個人 or 共用ミッションか")]
		public UnionMissionType MissionType { get; set; }

		[Description("ミッションの受注状態")]
		public UnionMissionPanelStatus Status { get; set; }

		[Description("現在の達成値")]
		public long ProgressCount { get; set; }

		[Description("ボーナス倍率")]
		public int BonusRate { get; set; }

		[Description("受注しているプレイヤーID")]
		public long AcceptedPlayerId { get; set; }

		[Description("受注日時")]
		public long AcceptedTime { get; set; }

		[Description("達成期限")]
		public long LimitTime { get; set; }

		[Description("クールタイムが明ける日時")]
		public long CoolTimeEndTime { get; set; }

		[Description("手動でクールタイム解除可能か否か")]
		public bool IsCoolTimeResettable { get; set; }
	}
}
