/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserDungeonBattleCharacterDtoInfo } from "./userDungeonBattleCharacterDtoInfo";
import { UserDungeonBattleDtoInfo } from "./userDungeonBattleDtoInfo";
import { UserSyncData } from "./userSyncData";

export class DungeonBattleUseRecoveryItemResponse extends ApiResponseBase implements IUserSyncApiResponse {
    userDungeonBattleCharacterDtoInfos: UserDungeonBattleCharacterDtoInfo[];
    userDungeonBattleDtoInfo: UserDungeonBattleDtoInfo;
    userSyncData: UserSyncData;
}
