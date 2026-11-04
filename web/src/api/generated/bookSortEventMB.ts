/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTimeZone } from "./iHasStartEndTimeZone";
import { StartEndTimeZoneType } from "./startEndTimeZoneType";
import { WinningLotteryCoefficient } from "./winningLotteryCoefficient";
import { MypageIconDisplayLocationType } from "./mypageIconDisplayLocationType";

export class BookSortEventMB extends MasterBookBase implements IHasStartEndTimeZone {
    startEndTimeZoneType: StartEndTimeZoneType;
    startTime: string;
    endTime: string;
    targetMissionIdList: number[];
    transferShopTabId: number;
    bonusFloorRepeat: number;
    bonusFloorList: number[];
    lotteryWinCoefficientListA: WinningLotteryCoefficient[];
    lotteryWinCoefficientListB: WinningLotteryCoefficient[];
    lotteryWinCoefficientListC: WinningLotteryCoefficient[];
    mypageIconDisplayLocationType: MypageIconDisplayLocationType;
}
