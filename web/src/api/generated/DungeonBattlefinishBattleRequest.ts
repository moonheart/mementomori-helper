/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { DungeonBattleIDungeonBattleRequest } from "./DungeonBattleiDungeonBattleRequest";

export class DungeonBattleFinishBattleRequest extends ApiRequestBase implements DungeonBattleIDungeonBattleRequest {
    dungeonGridGuid: string;
    visitDungeonCount: number;
    currentTermId: number;
}
