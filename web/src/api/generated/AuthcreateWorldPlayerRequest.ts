/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { IHasSteamTicketApiRequest } from "./iHasSteamTicketApiRequest";

export class AuthCreateWorldPlayerRequest extends ApiRequestBase implements IHasSteamTicketApiRequest {
    worldId: number;
    comment: string;
    name: string;
    steamTicket: string;
}
