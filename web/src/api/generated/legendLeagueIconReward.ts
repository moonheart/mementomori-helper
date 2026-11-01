/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserItem } from "./userItem";
import { LegendLeagueIconRewardLimitTradeType } from "./legendLeagueIconRewardLimitTradeType";
import { LegendLeagueIconRewardDisplayTabType } from "./legendLeagueIconRewardDisplayTabType";

export class LegendLeagueIconReward {
    id: number;
    startLocalTime: number;
    endLocalTime: number;
    consumeItemCount: number;
    giveItem: UserItem;
    limitTradeType: LegendLeagueIconRewardLimitTradeType;
    tradeCount: number;
    limitTradeCount: number;
    order: number;
    displayTabType: LegendLeagueIconRewardDisplayTabType;
    isNew: boolean;
}
