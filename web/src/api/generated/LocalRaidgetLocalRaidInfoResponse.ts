/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { ILocalRaidInfoApiResponse } from "./iLocalRaidInfoApiResponse";
import { LocalRaidQuestInfo } from "./localRaidQuestInfo";
import { LocalRaidEnemyInfo } from "./localRaidEnemyInfo";
import { UserSyncData } from "./userSyncData";

export class LocalRaidGetLocalRaidInfoResponse extends ApiResponseBase implements IUserSyncApiResponse, ILocalRaidInfoApiResponse {
    openLocalRaidQuestIds: number[];
    openEventLocalRaidQuestIds: number[];
    localRaidQuestInfos: LocalRaidQuestInfo[];
    localRaidEnemyInfos: LocalRaidEnemyInfo[];
    clearCountDict: { [key: number]: number; };
    userSyncData: UserSyncData;
}
