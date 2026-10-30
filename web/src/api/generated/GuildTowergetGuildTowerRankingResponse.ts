/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { GuildRank } from "./guildRank";
import { GuildTowerTotalStarRankingInfo } from "./guildTowerTotalStarRankingInfo";
import { JobFlags } from "./jobFlags";
import { GuildTowerReinforcementJobRankingData } from "./guildTowerReinforcementJobRankingData";

export class GuildTowerGetGuildTowerRankingResponse extends ApiResponseBase {
    currentGuildRanking: number;
    currentTotalStarRanking: number;
    maxClearedFloorMBId: number;
    todayGuildRanking: number;
    todayTotalStarRanking: number;
    topGuildRankList: GuildRank[];
    topTotalStarRankingPlayerList: GuildTowerTotalStarRankingInfo[];
    totalStarCount: number;
    reinforcementJobRankingDataListMap: { [key in JobFlags]?: GuildTowerReinforcementJobRankingData[]; };
}
