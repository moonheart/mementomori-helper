/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BattleEndInfo } from "./battleEndInfo";
import { BattleField } from "./battleField";
import { BattleLog } from "./battleLog";
import { BattleCharacterReport } from "./battleCharacterReport";
import { ShareCharacterOwnerInfo } from "./shareCharacterOwnerInfo";

export class BattleSimulationResult {
    battleEndInfo: BattleEndInfo;
    battleField: BattleField;
    battleLog: BattleLog;
    battleToken: string;
    battleCharacterReports: BattleCharacterReport[];
    shareCharacterOwnerInfo: ShareCharacterOwnerInfo;
}
