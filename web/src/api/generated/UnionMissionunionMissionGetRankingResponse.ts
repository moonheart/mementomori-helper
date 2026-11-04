/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { UnionMissionGuildRankingEntry } from "./unionMissionGuildRankingEntry";
import { UnionMissionPersonalRankingEntry } from "./unionMissionPersonalRankingEntry";

export class UnionMissionUnionMissionGetRankingResponse extends ApiResponseBase {
    guildRank: number;
    todayGuildRank: number;
    guildPoint: number;
    guildRankingList: UnionMissionGuildRankingEntry[];
    personalRank: number;
    todayPersonalRank: number;
    personalPoint: number;
    personalAcceptedCount: number;
    personalAchievedCount: number;
    personalRankingList: UnionMissionPersonalRankingEntry[];
}
