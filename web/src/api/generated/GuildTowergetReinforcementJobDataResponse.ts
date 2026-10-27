/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { JobFlags } from "./jobFlags";
import { GuildTowerReinforcementJobRankingData } from "./guildTowerReinforcementJobRankingData";
import { GuildTowerReinforcementJobData } from "./guildTowerReinforcementJobData";

export class GuildTowerGetReinforcementJobDataResponse extends ApiResponseBase {
    rankingDataListMap: { [key in JobFlags]?: GuildTowerReinforcementJobRankingData[]; };
    reinforcementJobDataList: GuildTowerReinforcementJobData[];
}
