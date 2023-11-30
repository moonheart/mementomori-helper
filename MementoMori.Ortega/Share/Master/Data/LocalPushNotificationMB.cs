using System.ComponentModel;
using MementoMori.Ortega.Share.Enums;
using MementoMori.Ortega.Share.Master.Attributes;
using MementoMori.Ortega.Share.Utils;
using MessagePack;

namespace MementoMori.Ortega.Share.Master.Data
{
	[Description("ローカル通知")]
	[MessagePackObject(true)]
	public class LocalPushNotificationMB : MasterBookBase, IHasStartEndTime
	{
		[Description("グループID")]
		[PropertyOrder(2)]
		public int GroupId
		{
			get;
		}

		[PropertyOrder(8)]
		[Description("送信条件")]
		public LocalNotificationSendType LocalNotificationSendType
		{
			get;
		}

		[PropertyOrder(1)]
		[Description("通知タイプ")]
		public LocalNotificationType LocalNotificationType
		{
			get;
		}

		[PropertyOrder(7)]
		[Description("メッセージ")]
		public string MessageKey
		{
			get;
		}

		[Description("優先度")]
		[PropertyOrder(5)]
		public int Priority
		{
			get;
		}

		[PropertyOrder(9)]
        [TimeSpanString]
		[Description("送信時刻")]
		public string SendTime
		{
			get;
		}

		[PropertyOrder(6)]
		[Description("タイトル")]
		public string TitleKey
		{
			get;
		}

		[SerializationConstructor]
		public LocalPushNotificationMB(long id, bool? isIgnore, string memo, LocalNotificationType localNotificationType, int groupId, string startTime, string endTime, int priority, string titleKey, string messageKey, LocalNotificationSendType localNotificationSendType, string sendTime)
			:base(id, isIgnore, memo)
		{
			LocalNotificationType = localNotificationType;
			GroupId = groupId;
			StartTime = startTime;
			EndTime = endTime;
			Priority = priority;
			TitleKey = titleKey;
			MessageKey = messageKey;
			LocalNotificationSendType = localNotificationSendType;
			SendTime = sendTime;
		}

		public LocalPushNotificationMB() :base(0L, false, ""){}

		[PropertyOrder(4)]
		[Description("終了時間")]
		public string EndTime
		{
			get;
		}

		[Description("開始時間")]
		[PropertyOrder(3)]
		public string StartTime
		{
			get;
		}
	}
}
