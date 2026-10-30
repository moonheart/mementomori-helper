/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { LocalRaidRoomConditionsType } from "./localRaidRoomConditionsType";
import { LocalRaidBattleLogPlayerInfo } from "./localRaidBattleLogPlayerInfo";

export class LocalRaidPartyInfo {
    conditionsType: LocalRaidRoomConditionsType;
    leaderPlayerId: number;
    leaderPlayerName: string;
    localRaidBattleLogPlayerInfoList: LocalRaidBattleLogPlayerInfo[];
    password: number;
    worldId: number;
    questId: number;
    requiredBattlePower: number;
    roomId: string;
    totalBattlePower: number;
    isAutoStart: boolean;
    isReady: boolean;
}
