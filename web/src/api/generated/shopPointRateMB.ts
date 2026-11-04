/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { IHasStartEndTime } from "./iHasStartEndTime";
import { ShopPayType } from "./shopPayType";

export class ShopPointRateMB extends MasterBookBase implements IHasStartEndTime {
    savePointRate: number;
    shopPayType: ShopPayType;
    startTime: string;
    endTime: string;
}
