/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { StripePointType } from "./stripePointType";
import { StripePaidType } from "./stripePaidType";

export class UserStripePointHistoryInfo {
    playerId: number;
    buyDateTime: Date;
    price: number;
    discountPrice: number;
    beforePoint: number;
    afterPoint: number;
    savePoint: number;
    usePoint: number;
    productNameKey: string;
    transactionId: string;
    stripePointType: StripePointType;
    stripePaidType: StripePaidType;
    cardSubInfo: string;
    refundDateTime: string;
    chargeBackDateTime: string;
}
