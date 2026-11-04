/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { TowerType } from "./towerType";
import { PlayerInfo } from "./playerInfo";
import { UserSyncData } from "./userSyncData";

export class RankingGetTowerRankingResponse extends ApiResponseBase implements IUserSyncApiResponse {
    nowRankingMap: { [key in TowerType]?: number; };
    todayRankingMap: { [key in TowerType]?: number; };
    topPlayerInfoListMap: { [key in TowerType]?: PlayerInfo[]; };
    topPlayerMaxClearQuestIdMap: { [key in TowerType]?: { [key: number]: number; }; };
    userSyncData: UserSyncData;
}
