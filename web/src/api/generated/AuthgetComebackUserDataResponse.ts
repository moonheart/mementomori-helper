/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { PlayerDataInfo } from "./playerDataInfo";

export class AuthGetComebackUserDataResponse extends ApiResponseBase {
    isReservedAccountDeletion: boolean;
    lastLoginPlayerDataInfo: PlayerDataInfo;
    oneTimeToken: string;
    userId: number;
}
