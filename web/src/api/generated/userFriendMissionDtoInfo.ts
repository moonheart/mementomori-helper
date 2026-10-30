/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MissionAchievementType } from "./missionAchievementType";
import { MissionStatusType } from "./missionStatusType";

export class UserFriendMissionDtoInfo {
    friendCampaignId: number;
    achievementType: MissionAchievementType;
    progressCount: number;
    missionStatusHistory: { [key in MissionStatusType]?: number[]; };
}
