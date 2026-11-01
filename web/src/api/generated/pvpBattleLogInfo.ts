/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserCharacterInfo } from "./userCharacterInfo";
import { BattleEndInfo } from "./battleEndInfo";
import { PlayerInfo } from "./playerInfo";

export class PvpBattleLogInfo {
    attackBattlePower: number;
    attackCharacterInfoList: UserCharacterInfo[];
    battleEndInfo: BattleEndInfo;
    battleTime: number;
    battleToken: string;
    defenseBattlePower: number;
    defenseCharacterInfoList: UserCharacterInfo[];
    newRank: number;
    oldRank: number;
    rivalPlayerInfo: PlayerInfo;
}
