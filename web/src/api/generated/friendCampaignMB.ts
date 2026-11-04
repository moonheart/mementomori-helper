/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { ICharacterImage } from "./iCharacterImage";
import { UserItem } from "./userItem";

export class FriendCampaignMB extends MasterBookBase implements IHasStartEndTime, ICharacterImage {
    codeExpirationPeriod: number;
    codeStartTime: string;
    codeLimitCount: number;
    friendMissionIdList: number[];
    rewardItemList: UserItem[];
    title: string;
    characterImageId: number;
    characterImageX: number;
    characterImageY: number;
    characterImageSize: number;
    endTime: string;
    startTime: string;
}
