/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { TowerType } from "./towerType";

export class TowerBattleStartRequest extends ApiRequestBase {
    targetTowerType: TowerType;
    towerBattleQuestId: number;
}
