/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IGuildSyncApiResponse } from "./iGuildSyncApiResponse";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GuildTowerComboData } from "./guildTowerComboData";
import { GuildTowerEntryCharacter } from "./guildTowerEntryCharacter";
import { GuildTowerReinforcementJobData } from "./guildTowerReinforcementJobData";
import { GuildSyncData } from "./guildSyncData";
import { UserSyncData } from "./userSyncData";

export class GuildTowerGetGuildTowerInfoResponse extends ApiResponseBase implements IGuildSyncApiResponse, IUserSyncApiResponse {
    clearGaugeProgress: number;
    clearPartyBattlePowerCoefficientMap: { [key: number]: number; };
    comboData: GuildTowerComboData;
    currentFloorMBId: number;
    enemyRankCoefficientMap: { [key: number]: number; };
    guildTowerEntryCharacterList: GuildTowerEntryCharacter[];
    isAlreadyEntryToday: boolean;
    isChangeDayFromFirstWin: boolean;
    isContinueEntry: boolean;
    lastSelectedDifficulty: number;
    lastWinEntryPassedDays: number;
    reinforcementJobDataList: GuildTowerReinforcementJobData[];
    reinforcementMaterialDropCoefficientMap: { [key: number]: number; };
    todayWinCount: number;
    lastReinforceLocalTimeStamp: number;
    guildSyncData: GuildSyncData;
    userSyncData: UserSyncData;
}
