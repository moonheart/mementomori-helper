/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { JobFlags } from "./jobFlags";
import { UserItem } from "./userItem";

export class GuildTowerReinforceJobRequest extends ApiRequestBase {
    jobFlags: JobFlags;
    level: number;
    materialItemList: UserItem[];
}
