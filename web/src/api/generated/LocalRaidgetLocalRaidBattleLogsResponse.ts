/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { ILocalRaidInfoApiResponse } from "./iLocalRaidInfoApiResponse";
import { LocalRaidBattleLogInfo } from "./localRaidBattleLogInfo";
import { LocalRaidQuestInfo } from "./localRaidQuestInfo";
import { LocalRaidEnemyInfo } from "./localRaidEnemyInfo";

export class LocalRaidGetLocalRaidBattleLogsResponse extends ApiResponseBase implements ILocalRaidInfoApiResponse {
    localRaidBattleLogInfoList: LocalRaidBattleLogInfo[];
    localRaidQuestInfos: LocalRaidQuestInfo[];
    localRaidEnemyInfos: LocalRaidEnemyInfo[];
}
