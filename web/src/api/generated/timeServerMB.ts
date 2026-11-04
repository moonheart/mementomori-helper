/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MasterBookBase } from "./masterBookBase";
import { TimeServerType } from "./timeServerType";
import { LanguageType } from "./languageType";

export class TimeServerMB extends MasterBookBase {
    timeServerType: TimeServerType;
    countryCodeList: string[];
    defaultLanguageType: LanguageType;
    defaultVoiceLanguageType: LanguageType;
    displayNameKey: string;
    name: string;
    differenceDateTimeFromUtc: string;
}
