/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { UserItem } from "./userItem";
import { TowerType } from "./towerType";

export class TowerBattleQuestMB extends MasterBookBase {
    baseClearPartyDeckPower: number;
    battleRewardsConfirmed: UserItem[];
    battleRewardsFirst: UserItem[];
    enemyIds: number[];
    floor: number;
    lotteryRewardInfoId: number;
    towerType: TowerType;
}
