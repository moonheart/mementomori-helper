/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { BountyQuestInfo } from "./bountyQuestInfo";
import { UserBountyQuestBoardDtoInfo } from "./userBountyQuestBoardDtoInfo";
import { UserBountyQuestDtoInfo } from "./userBountyQuestDtoInfo";
import { UserBountyQuestMemberDtoInfo } from "./userBountyQuestMemberDtoInfo";

export class BountyQuestGetListResponse extends ApiResponseBase {
    bountyQuestInfos: BountyQuestInfo[];
    changeDayTime: number;
    userBoardRank: number;
    userBountyQuestBoardDtoInfos: UserBountyQuestBoardDtoInfo[];
    userBountyQuestDtoInfos: UserBountyQuestDtoInfo[];
    selfUserBountyQuestMemberDtoInfos: UserBountyQuestMemberDtoInfo[];
    friendAndGuildMemberUserBountyQuestMemberDtoInfos: UserBountyQuestMemberDtoInfo[];
}
