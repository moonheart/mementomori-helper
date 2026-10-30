/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { IHasSteamTicketApiRequest } from "./iHasSteamTicketApiRequest";

export class AuthComebackUserRequest extends ApiRequestBase implements IHasSteamTicketApiRequest {
    fromUserId: number;
    oneTimeToken: string;
    toUserId: number;
    steamTicket: string;
}
