/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { JobFlags } from "./jobFlags";

export class GuildTowerGetReinforcementJobDataRequest extends ApiRequestBase {
    jobLevelMap: { [key in JobFlags]?: number; };
}
