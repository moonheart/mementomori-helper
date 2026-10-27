/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { AppAssetVersionInfo } from "./appAssetVersionInfo";
import { WorldInfo } from "./worldInfo";
import { MaintenanceDebugUserInfo } from "./maintenanceDebugUserInfo";
import { MaintenanceInfo } from "./maintenanceInfo";
import { ManagementNewUserInfo } from "./managementNewUserInfo";
import { TitleInfo } from "./titleInfo";

export class AuthGetDataUriResponse extends ApiResponseBase {
    appAssetVersionInfo: AppAssetVersionInfo;
    worldInfos: WorldInfo[];
    maintenanceDebugUserInfos: MaintenanceDebugUserInfo[];
    maintenanceInfos: MaintenanceInfo[];
    managementNewUserInfos: ManagementNewUserInfo[];
    assetCatalogUriFormat: string;
    assetCatalogFixedUriFormat: string;
    masterUriFormat: string;
    noticeBannerImageUriFormat: string;
    rawDataUriFormat: string;
    titleInfo: TitleInfo;
}
