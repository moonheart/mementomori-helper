/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { TradeShopAutoUpdateType } from "./tradeShopAutoUpdateType";
import { ConsumeItemInfo } from "./consumeItemInfo";
import { CustomTextLayout } from "./customTextLayout";
import { OpenCommandType } from "./openCommandType";
import { TradeShopType } from "./tradeShopType";

export class TradeShopTabMB extends MasterBookBase implements IHasStartEndTime {
    autoUpdateTimes: number[];
    autoUpdateType: TradeShopAutoUpdateType;
    consumeItemInfos: ConsumeItemInfo[];
    customTextLayout: CustomTextLayout;
    decorationColor: string;
    decorationId: number;
    decorationSpecialId: number;
    forceResetTimeFixJST: string;
    iconId: number;
    isHide: boolean;
    isHideNotOpen: boolean;
    isManualUpdate: boolean;
    manualUpdateCurrencyCount: number;
    openCommandType: OpenCommandType;
    sortOrder: number;
    tabNameKey: string;
    tradeShopItemCount: number;
    tradeShopType: TradeShopType;
    endTime: string;
    startTime: string;
}
