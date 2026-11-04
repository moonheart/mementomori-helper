/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { AchieveRankingPlayerInfo } from "./achieveRankingPlayerInfo";
import { AchieveRewardReceivedPlayerInfo } from "./achieveRewardReceivedPlayerInfo";
import { ClearPartyCharacterInfo } from "./clearPartyCharacterInfo";

export class RankingGetAchieveRankingPlayerInfoResponse extends ApiResponseBase {
    achieveRankingPlayerInfoList: AchieveRankingPlayerInfo[];
    noRecordMBIdList: number[];
    receivedAchieveRewardPlayerInfoList: AchieveRewardReceivedPlayerInfo[];
    receivedAchieveRewardPlayerNameList: string[];
    firstRankPlayerId: number;
    firstRankClearPartyCharacterInfos: ClearPartyCharacterInfo[];
    isCheckedAwardPresentation: boolean;
}
