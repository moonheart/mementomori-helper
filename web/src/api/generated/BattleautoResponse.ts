/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { UserBattleAutoDtoInfo } from "./userBattleAutoDtoInfo";

export class BattleAutoResponse extends ApiResponseBase {
    userBattleAuto: UserBattleAutoDtoInfo;
    remainNextRankUpTime: number;
}
