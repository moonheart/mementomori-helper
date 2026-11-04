/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { UserItem } from "./userItem";
import { DungeonBattleLayer } from "./dungeonBattleLayer";
import { UserDungeonBattleShopDtoInfo } from "./userDungeonBattleShopDtoInfo";
import { UserDungeonBattleDtoInfo } from "./userDungeonBattleDtoInfo";
import { UserSyncData } from "./userSyncData";

export class DungeonBattleSkipDungeonBattleResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardItemList: UserItem[];
    rewardRelicIdList: number[];
    currentDungeonBattleLayer: DungeonBattleLayer;
    userDungeonBattleShopDtoInfo: UserDungeonBattleShopDtoInfo;
    userDungeonBattleDtoInfo: UserDungeonBattleDtoInfo;
    userSyncData: UserSyncData;
}
