/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserDungeonBattleCharacterDtoInfo } from "./userDungeonBattleCharacterDtoInfo";
import { UserSyncData } from "./userSyncData";

export class DungeonBattleFinishBattleResponse extends ApiResponseBase implements IUserSyncApiResponse {
    userDungeonBattleCharacterDtoInfos: UserDungeonBattleCharacterDtoInfo[];
    userSyncData: UserSyncData;
}
