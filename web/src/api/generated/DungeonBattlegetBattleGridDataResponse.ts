/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { DungeonBattleEnemyInfo } from "./dungeonBattleEnemyInfo";
import { UserItem } from "./userItem";

export class DungeonBattleGetBattleGridDataResponse extends ApiResponseBase {
    enemyInfos: DungeonBattleEnemyInfo[];
    normalRewardItemList: UserItem[];
    specialRewardItemList: UserItem[];
}
