/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { LimitedLoginBonusRewardItem } from "./limitedLoginBonusRewardItem";
import { UserSyncData } from "./userSyncData";

export class LoginBonusReceiveDailyLimitedLoginBonusResponse extends ApiResponseBase implements IUserSyncApiResponse {
    rewardItemList: LimitedLoginBonusRewardItem[];
    receivedSwitchingDailyRewardIdList: number[];
    userSyncData: UserSyncData;
}
