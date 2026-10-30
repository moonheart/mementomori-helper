/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { GuildRaidInfo } from "./guildRaidInfo";

export class GuildRaidGetGuildRaidInfoResponse extends ApiResponseBase {
    guildRaidInfos: GuildRaidInfo[];
    canChallengeReleasableBossTimeStamp: number;
}
