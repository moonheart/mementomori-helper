/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { IHasSteamTicketApiRequest } from "./iHasSteamTicketApiRequest";
import { LanguageType } from "./languageType";

export class AuthCreateUserRequest extends ApiRequestBase implements IHasSteamTicketApiRequest {
    adverisementId: string;
    appVersion: string;
    countryCode: string;
    deviceToken: string;
    displayLanguage: LanguageType;
    modelName: string;
    oSVersion: string;
    steamTicket: string;
    authToken: number;
}
