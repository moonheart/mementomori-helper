/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { MaintenanceServerType } from "./maintenanceServerType";
import { MaintenanceAreaType } from "./maintenanceAreaType";

export class MaintenanceInfo {
    maintenanceServerType: MaintenanceServerType;
    startTimeFixJST: Date;
    endTimeFixJST: Date;
    maintenancePlatformTypes: number[];
    maintenanceAreaType: MaintenanceAreaType;
    areaIds: number[];
    maintenanceFunctionTypes: number[];
}
