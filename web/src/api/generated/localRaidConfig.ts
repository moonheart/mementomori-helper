/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { WeightedItem } from "./weightedItem";

export class LocalRaidConfig {
    rewardItems: WeightedItem[] = [];
    selfCreateRoom: boolean;
    waitSeconds: number = 3;
    joinWaitMinutes: number = 10;
    hostWhenNoRoom: boolean;
    maxSoloLosses: number = 3;
    roomWaitMinutes: number = 3;
    questLevel: number;
    dropToLowerQuest: boolean;
    lowerQuestAfterMinutes: number = 3;
    maxTaskMinutes: number = 20;
}
