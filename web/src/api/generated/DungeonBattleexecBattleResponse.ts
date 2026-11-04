/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { BattleRewardResult } from "./battleRewardResult";
import { BattleSimulationResult } from "./battleSimulationResult";
import { DungeonBattleAllyInfo } from "./dungeonBattleAllyInfo";

export class DungeonBattleExecBattleResponse extends ApiResponseBase {
    battleRewardResult: BattleRewardResult;
    battleSimulationResult: BattleSimulationResult;
    dungeonBattleAllyInfos: DungeonBattleAllyInfo[];
    dungeonCoinCountToReset: number;
}
