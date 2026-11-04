using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Utils
{
	public static class QuestQuickTicketUtil
	{
		public static int GetQuestQuickTicketTime(QuestQuickTicketType questQuickTicketType)
		{
			if (questQuickTicketType <= QuestQuickTicketType.LuxuryHours8)
			{
			}
			return 0;
		}

		public static QuestQuickTicketRewardFlags GetQuestQuickTicketRewardItemFlags(QuestQuickTicketType questQuickTicketType)
		{
			switch (questQuickTicketType)
			{
				case QuestQuickTicketType.GoldHours1:
				case QuestQuickTicketType.GoldHours2:
				case QuestQuickTicketType.GoldHours6:
				case QuestQuickTicketType.GoldHours8:
				case QuestQuickTicketType.GoldHours24:
					return QuestQuickTicketRewardFlags.Gold;
				case QuestQuickTicketType.ExpHours1:
				case QuestQuickTicketType.ExpHours2:
				case QuestQuickTicketType.ExpHours6:
				case QuestQuickTicketType.ExpHours8:
				case QuestQuickTicketType.ExpHours24:
					return QuestQuickTicketRewardFlags.CharacterExp;
				case QuestQuickTicketType.SeedHours1:
				case QuestQuickTicketType.SeedHours2:
				case QuestQuickTicketType.SeedHours6:
				case QuestQuickTicketType.SeedHours8:
				case QuestQuickTicketType.SeedHours24:
					return QuestQuickTicketRewardFlags.Seed;
				case QuestQuickTicketType.LuxuryHours1:
				case QuestQuickTicketType.LuxuryHours2:
				case QuestQuickTicketType.LuxuryHours6:
				case QuestQuickTicketType.LuxuryHours8:
				case QuestQuickTicketType.LuxuryHours24:
					return QuestQuickTicketRewardFlags.Gold
						| QuestQuickTicketRewardFlags.CharacterExp
						| QuestQuickTicketRewardFlags.Seed;
				default:
					return QuestQuickTicketRewardFlags.None;
			}
		}
	}
}
