/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { LegendLeagueIconReward } from "./legendLeagueIconReward";

export class LegendLeagueGetLegendLeagueIconRewardInfoResponse extends ApiResponseBase {
    isCurrentTimeInPrevRankRewardPeriod: boolean;
    legendLeagueIconRewardList: LegendLeagueIconReward[];
    selectedIconRewardId: number;
}
