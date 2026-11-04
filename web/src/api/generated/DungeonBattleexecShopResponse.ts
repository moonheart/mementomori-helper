/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserDungeonBattleDtoInfo } from "./userDungeonBattleDtoInfo";
import { UserDungeonBattleShopDtoInfo } from "./userDungeonBattleShopDtoInfo";
import { UserSyncData } from "./userSyncData";

export class DungeonBattleExecShopResponse extends ApiResponseBase implements IUserSyncApiResponse {
    userDungeonBattleDtoInfo: UserDungeonBattleDtoInfo;
    userDungeonBattleShopDtoInfo: UserDungeonBattleShopDtoInfo;
    userSyncData: UserSyncData;
}
