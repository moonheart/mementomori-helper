/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { PlayerInfo } from "./playerInfo";

export class LocalRaidGetLocalRaidFriendInfoResponse extends ApiResponseBase {
    playerChallengeCountDict: { [key: number]: number; };
    playerInfoList: PlayerInfo[];
}
