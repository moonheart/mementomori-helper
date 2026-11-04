/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiRequestBase } from "./apiRequestBase";
import { SphereChangeInfo } from "./sphereChangeInfo";

export class EquipmentChangeSphereRequest extends ApiRequestBase {
    sphereChangeInfos: SphereChangeInfo[];
    isFromSphereEquipmentListView: boolean;
}
