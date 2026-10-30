/**
 * This is a TypeGen auto-generated file.
 * Any changes made to this file can be lost when this file is regenerated.
 */

import { ApiResponseBase } from "./apiResponseBase";
import { IErrorResponse } from "./iErrorResponse";
import { ErrorCode } from "./errorCode";
import { ErrorHandlingType } from "./errorHandlingType";

export class ApiErrorResponse extends ApiResponseBase implements IErrorResponse {
    errorCode: ErrorCode;
    message: string;
    errorHandlingType: ErrorHandlingType;
    errorMessageId: number;
    messageParams: string[];
}
