/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { MapPlayerInfo } from "./mapPlayerInfo";
import { UserMapBuildingDtoInfo } from "./userMapBuildingDtoInfo";

export class QuestMapInfoResponse extends ApiResponseBase {
    autoBattleDropEquipmentPercent: number;
    currentQuestAutoEnemyIds: number[];
    mapOtherPlayerInfos: MapPlayerInfo[];
    mapPlayerInfos: MapPlayerInfo[];
    userMapBuildingDtoInfos: UserMapBuildingDtoInfo[];
}
