/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { UnionMissionType } from "./unionMissionType";
import { UnionMissionPanelStatus } from "./unionMissionPanelStatus";

export class UnionMissionPanelInfo {
    missionPanelId: number;
    missionId: number;
    missionType: UnionMissionType;
    status: UnionMissionPanelStatus;
    progressCount: number;
    bonusRate: number;
    acceptedPlayerId: number;
    acceptedTime: number;
    limitTime: number;
    coolTimeEndTime: number;
    isCoolTimeResettable: boolean;
}
