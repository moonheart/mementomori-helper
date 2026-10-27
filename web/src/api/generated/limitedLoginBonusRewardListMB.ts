/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { DailyLimitedLoginBonusItem } from "./dailyLimitedLoginBonusItem";
import { EveryDayLimitedLoginBonusItem } from "./everyDayLimitedLoginBonusItem";
import { SpecialLimitedLoginBonusItem } from "./specialLimitedLoginBonusItem";

export class LimitedLoginBonusRewardListMB extends MasterBookBase {
    dailyRewardList: DailyLimitedLoginBonusItem[];
    everyDayRewardItem: EveryDayLimitedLoginBonusItem;
    existEveryDayReward: boolean;
    existSpecialReward: boolean;
    specialRewardItem: SpecialLimitedLoginBonusItem;
}
