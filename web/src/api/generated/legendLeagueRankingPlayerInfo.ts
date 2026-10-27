/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { BaseParameter } from "./baseParameter";
import { BattleParameter } from "./battleParameter";
import { UserEquipmentDtoInfo } from "./userEquipmentDtoInfo";
import { PlayerInfo } from "./playerInfo";
import { UserCharacterDtoInfo } from "./userCharacterDtoInfo";

export class LegendLeagueRankingPlayerInfo {
    consecutiveVictoryCount: number;
    currentPoint: number;
    currentRank: number;
    defenseBattlePower: number;
    defenseCharacterBaseParameterMap: { [key: string]: BaseParameter; };
    defenseCharacterBattleParameterMap: { [key: string]: BattleParameter; };
    defenseEquipmentDtoInfoListMap: { [key: string]: UserEquipmentDtoInfo[]; };
    playerInfo: PlayerInfo;
    userCharacterDtoInfoList: UserCharacterDtoInfo[];
}
