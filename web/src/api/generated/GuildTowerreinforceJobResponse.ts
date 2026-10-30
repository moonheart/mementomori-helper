/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserItem } from "./userItem";
import { GuildTowerReinforcementJobRankingData } from "./guildTowerReinforcementJobRankingData";
import { GuildTowerReinforcementJobData } from "./guildTowerReinforcementJobData";
import { UserSyncData } from "./userSyncData";

export class GuildTowerReinforceJobResponse extends ApiResponseBase implements IUserSyncApiResponse {
    notConsumedItemList: UserItem[];
    rankingDataList: GuildTowerReinforcementJobRankingData[];
    reinforcementJobData: GuildTowerReinforcementJobData;
    lastReinforceLocalTimeStamp: number;
    userSyncData: UserSyncData;
}
