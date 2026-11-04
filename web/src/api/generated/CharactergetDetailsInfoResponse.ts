/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiErrorResponse } from "./apiErrorResponse";
import { IErrorResponse } from "./iErrorResponse";
import { CharacterDetailInfo } from "./characterDetailInfo";

export class CharacterGetDetailsInfoResponse extends ApiErrorResponse implements IErrorResponse {
    characterDetailInfos: CharacterDetailInfo[];
}
