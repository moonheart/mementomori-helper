/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { SnsType } from "./snsType";

export class AuthGetComebackUserDataRequest extends ApiRequestBase {
    appleIdToken: string;
    fromUserId: number;
    googleAuthorizationCode: string;
    password: string;
    snsType: SnsType;
    twitterAccessToken: string;
    twitterAccessTokenSecret: string;
    userId: number;
    authToken: number;
}
