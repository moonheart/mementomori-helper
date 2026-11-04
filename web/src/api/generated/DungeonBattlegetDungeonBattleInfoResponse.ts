/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { DungeonBattleLayer } from "./dungeonBattleLayer";
import { ItemType } from "./itemType";
import { UserDungeonBattleCharacterDtoInfo } from "./userDungeonBattleCharacterDtoInfo";
import { UserDungeonBattleGuestCharacterDtoInfo } from "./userDungeonBattleGuestCharacterDtoInfo";
import { UserDungeonBattleShopDtoInfo } from "./userDungeonBattleShopDtoInfo";
import { UserDungeonBattleDtoInfo } from "./userDungeonBattleDtoInfo";
import { UserItem } from "./userItem";
import { UserSyncData } from "./userSyncData";

export class DungeonBattleGetDungeonBattleInfoResponse extends ApiResponseBase implements IUserSyncApiResponse {
    currentDungeonBattleLayer: DungeonBattleLayer;
    currentTermId: number;
    eventItemId: number;
    eventItemType: ItemType;
    eventNo: number;
    eventTutorialId: number;
    gridBattlePowerDict: { [key: string]: number; };
    rewardRelicIds: number[];
    userDungeonBattleCharacterDtoInfos: UserDungeonBattleCharacterDtoInfo[];
    userDungeonBattleGuestCharacterDtoInfos: UserDungeonBattleGuestCharacterDtoInfo[];
    userDungeonBattleMissedCount: number;
    userDungeonBattleShopDtoInfos: UserDungeonBattleShopDtoInfo[];
    userDungeonDtoInfo: UserDungeonBattleDtoInfo;
    skipRewardList: UserItem[];
    utcEndTimeStamp: number;
    userSyncData: UserSyncData;
}
