/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { PvpBattleLogInfo } from "./pvpBattleLogInfo";

export class BattleGetPvpBattleLogsResponse extends ApiResponseBase {
    attackPvpBattleLogInfoList: PvpBattleLogInfo[];
    defensePvpBattleLogInfoList: PvpBattleLogInfo[];
}
