/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { MissionAchievementType } from "./missionAchievementType";

export class FriendRewardFriendMissionRequest extends ApiRequestBase {
    friendCampaignId: number;
    achievementType: MissionAchievementType;
    friendMissionId: number;
}
