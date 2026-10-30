/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { IHasSteamTicketApiRequest } from "./iHasSteamTicketApiRequest";
import { ErrorLogInfo } from "./errorLogInfo";
import { MusicPlayerPlayLogInfo } from "./musicPlayerPlayLogInfo";

export class UserLoginPlayerRequest extends ApiRequestBase implements IHasSteamTicketApiRequest {
    password: string;
    playerId: number;
    errorLogInfoList: ErrorLogInfo[];
    musicPlayerPlayLogInfoList: MusicPlayerPlayLogInfo[];
    customMusicSettingPlayLogInfoList: MusicPlayerPlayLogInfo[];
    steamTicket: string;
}
