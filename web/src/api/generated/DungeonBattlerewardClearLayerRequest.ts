/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { DungeonBattleIDungeonBattleRequest } from "./DungeonBattleiDungeonBattleRequest";
import { DungeonBattleDifficultyType } from "./dungeonBattleDifficultyType";

export class DungeonBattleRewardClearLayerRequest extends ApiRequestBase implements DungeonBattleIDungeonBattleRequest {
    clearedLayer: number;
    dungeonBattleDifficultyType: DungeonBattleDifficultyType;
    currentTermId: number;
}
