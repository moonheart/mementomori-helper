/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UserCharacterDtoInfo } from "./userCharacterDtoInfo";
import { BattleEndInfo } from "./battleEndInfo";
import { PlayerInfo } from "./playerInfo";

export class LegendLeagueLogInfo {
    attackBattlePower: number;
    attackCharacterDtoInfoList: UserCharacterDtoInfo[];
    battleEndInfo: BattleEndInfo;
    battleTime: number;
    battleToken: string;
    defenseBattlePower: number;
    defenseCharacterDtoInfoList: UserCharacterDtoInfo[];
    getPoint: number;
    rivalPlayerInfo: PlayerInfo;
    targetConsecutiveVictoryCount: number;
    targetPoint: number;
}
