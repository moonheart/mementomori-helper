/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { NoticeAccessType } from "./noticeAccessType";
import { LanguageType } from "./languageType";

export class NoticeGetNoticeInfoListRequest extends ApiRequestBase {
    accessType: NoticeAccessType;
    countryCode: string;
    languageType: LanguageType;
    userId: number;
}
