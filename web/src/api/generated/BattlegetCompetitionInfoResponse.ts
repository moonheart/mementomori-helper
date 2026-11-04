/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { ItemType } from "./itemType";

export class BattleGetCompetitionInfoResponse extends ApiResponseBase {
    existDungeonBattleMissedCompensation: boolean;
    dungeonBattleMissedCount: number;
    isDungeonBattleEventOpen: boolean;
    dungeonBattleEventItemType: ItemType;
    dungeonBattleEventItemId: number;
}
