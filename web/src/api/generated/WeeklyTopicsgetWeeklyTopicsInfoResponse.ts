/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { WeeklyTopicsTopCharacterData } from "./weeklyTopicsTopCharacterData";
import { WeeklyTopicsPvpData } from "./weeklyTopicsPvpData";
import { WeeklyTopicsBossBattleData } from "./weeklyTopicsBossBattleData";
import { WeeklyTopicsShopData } from "./weeklyTopicsShopData";

export class WeeklyTopicsGetWeeklyTopicsInfoResponse extends ApiResponseBase {
    topCharacterDataList: WeeklyTopicsTopCharacterData[];
    battleLeagueData: WeeklyTopicsPvpData;
    legendLeagueData: WeeklyTopicsPvpData;
    bossBattleData: WeeklyTopicsBossBattleData;
    shopData: WeeklyTopicsShopData;
}
