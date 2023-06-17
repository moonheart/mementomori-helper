using MementoMori.Ortega.Share.Enums;

namespace MementoMori.Ortega.Share.Utils
{
	public static class MissionUtil
	{
		public static MissionGroupType MissionTypeToMissionGroup(MissionType missionType)
		{
			switch (missionType)
			{
				case MissionType.Main:
					return MissionGroupType.Main;
				case MissionType.Daily:
					return MissionGroupType.Daily;
				case MissionType.Weekly:
					return MissionGroupType.Weekly;
				case MissionType.BeginnerFirstDay:
				case MissionType.BeginnerFirstDayLevel:
				case MissionType.BeginnerFirstDayStage:
				case MissionType.BeginnerFirstDayBuy:
				case MissionType.BeginnerSecondDay:
				case MissionType.BeginnerSecondDayQuick:
				case MissionType.BeginnerSecondDayBattleLeague:
				case MissionType.BeginnerSecondDayBuy:
				case MissionType.BeginnerThirdDay:
				case MissionType.BeginnerThirdDayForge:
				case MissionType.BeginnerThirdDayDungeonBattle:
				case MissionType.BeginnerThirdDayBuy:
				case MissionType.BeginnerFourthDay:
				case MissionType.BeginnerFourthDayReinforceEquipment:
				case MissionType.BeginnerFourthDayTowerBattle:
				case MissionType.BeginnerFourthDayBuy:
				case MissionType.BeginnerFifthDay:
				case MissionType.BeginnerFifthDayBountyQuest:
				case MissionType.BeginnerFifthDayTotalCharacter:
				case MissionType.BeginnerFifthDayBuy:
				case MissionType.BeginnerSixthDay:
				case MissionType.BeginnerSixthDaySphere:
				case MissionType.BeginnerSixthDayCharacterEvolution:
				case MissionType.BeginnerSixthDayBuy:
				case MissionType.BeginnerSeventhDay:
				case MissionType.BeginnerSeventhDayTraining:
				case MissionType.BeginnerSeventhDayLocalRaid:
				case MissionType.BeginnerSeventhDayBuy:
					return MissionGroupType.Beginner;
				case MissionType.ComebackLogin:
				case MissionType.ComebackActivity:
				case MissionType.ComebackConsumeCurrency:
					return MissionGroupType.Comeback;
				case MissionType.NewCharacter:
					return MissionGroupType.NewCharacter;
				case MissionType.LimitedFirstDayTab1:
				case MissionType.LimitedFirstDayTab2:
				case MissionType.LimitedFirstDayTab3:
				case MissionType.LimitedFirstDayTab4:
				case MissionType.LimitedSecondDayTab1:
				case MissionType.LimitedSecondDayTab2:
				case MissionType.LimitedSecondDayTab3:
				case MissionType.LimitedSecondDayTab4:
				case MissionType.LimitedThirdDayTab1:
				case MissionType.LimitedThirdDayTab2:
				case MissionType.LimitedThirdDayTab3:
				case MissionType.LimitedThirdDayTab4:
				case MissionType.LimitedFourthDayTab1:
				case MissionType.LimitedFourthDayTab2:
				case MissionType.LimitedFourthDayTab3:
				case MissionType.LimitedFourthDayTab4:
				case MissionType.LimitedFifthDayTab1:
				case MissionType.LimitedFifthDayTab2:
				case MissionType.LimitedFifthDayTab3:
				case MissionType.LimitedFifthDayTab4:
				case MissionType.LimitedSixthDayTab1:
				case MissionType.LimitedSixthDayTab2:
				case MissionType.LimitedSixthDayTab3:
				case MissionType.LimitedSixthDayTab4:
				case MissionType.LimitedSeventhDayTab1:
				case MissionType.LimitedSeventhDayTab2:
				case MissionType.LimitedSeventhDayTab3:
				case MissionType.LimitedSeventhDayTab4:
					return MissionGroupType.Limited;
				default:
					throw new ArgumentOutOfRangeException(nameof(missionType), missionType, null);
			}
		}
	}
}
