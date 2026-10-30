/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IGuildSyncApiResponse } from "./iGuildSyncApiResponse";
import { IUserSyncApiResponse } from "./iUserSyncApiResponse";
import { GuildGvgInfo } from "./guildGvgInfo";
import { GuildSystemChatOptionInfo } from "./guildSystemChatOptionInfo";
import { GuildTowerComboData } from "./guildTowerComboData";
import { GuildSyncData } from "./guildSyncData";
import { UserSyncData } from "./userSyncData";

export class GuildGetGuildBaseInfoResponse extends ApiResponseBase implements IGuildSyncApiResponse, IUserSyncApiResponse {
    defaultGlobalGvgMatchingNumber: number;
    globalGuildGvgInfo: GuildGvgInfo;
    guildLoginBonusLevel: number;
    guildSystemChatOptionInfos: GuildSystemChatOptionInfo[];
    guildTowerComboData: GuildTowerComboData;
    guildTowerCurrentFloor: number;
    guildTowerTodayWinCount: number;
    isRaidOpen: boolean;
    localGuildGvgInfo: GuildGvgInfo;
    guildSyncData: GuildSyncData;
    userSyncData: UserSyncData;
}
