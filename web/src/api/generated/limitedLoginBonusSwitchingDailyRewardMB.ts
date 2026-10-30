/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { UserItem } from "./userItem";
import { CharacterRarityFlags } from "./characterRarityFlags";

export class LimitedLoginBonusSwitchingDailyRewardMB extends MasterBookBase {
    limitedLoginBonusRewardListId: number;
    date: number;
    minQuestId: number;
    maxQuestId: number;
    minVip: number;
    maxVip: number;
    dailyRewardItem: UserItem;
    rarityFlags: CharacterRarityFlags;
}
