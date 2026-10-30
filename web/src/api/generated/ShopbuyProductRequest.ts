/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { IHasSteamTicketApiRequest } from "./iHasSteamTicketApiRequest";
import { ShopProductType } from "./shopProductType";

export class ShopBuyProductRequest extends ApiRequestBase implements IHasSteamTicketApiRequest {
    givePlayerId: number;
    mbId: number;
    productId: string;
    receipt: string;
    shopProductType: ShopProductType;
    steamTicket: string;
}
