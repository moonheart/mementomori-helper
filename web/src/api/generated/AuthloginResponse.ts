/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { AccountMessageInfo } from "./accountMessageInfo";
import { RemoteNotificationType } from "./remoteNotificationType";
import { PlayerDataInfo } from "./playerDataInfo";
import { LanguageType } from "./languageType";
import { WarningMessageInfo } from "./warningMessageInfo";
import { SelectShopProductInfo } from "./selectShopProductInfo";
import { StripeShopProductInfo } from "./stripeShopProductInfo";
import { UserSyncData } from "./userSyncData";

export class AuthLoginResponse extends ApiResponseBase implements IUserSyncApiResponse {
    accountMessageInfo: AccountMessageInfo;
    accountMessageInfos: AccountMessageInfo[];
    isReservedAccountDeletion: boolean;
    ignoreTypes: RemoteNotificationType[];
    maxVip: number;
    playerDataInfoList: PlayerDataInfo[];
    recommendWorldId: number;
    textLanguageType: LanguageType;
    voiceLanguageType: LanguageType;
    warningMessageInfos: WarningMessageInfo[];
    worldIdList: number[];
    selectShopProductInfoDict: { [key: number]: SelectShopProductInfo[]; };
    stripeShopProductInfo: StripeShopProductInfo;
    specialWorldDict: { [key: number]: string; };
    userSyncData: UserSyncData;
}
