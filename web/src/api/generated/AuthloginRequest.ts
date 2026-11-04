/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";

export class AuthLoginRequest extends ApiRequestBase {
    clientKey: string;
    deviceToken: string;
    appVersion: string;
    oSVersion: string;
    modelName: string;
    adverisementId: string;
    userId: number;
    isPushNotificationAllowed: boolean;
}
