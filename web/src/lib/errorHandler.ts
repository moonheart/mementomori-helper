import { AxiosError } from 'axios';
import { ErrorCode } from '@/api/generated/errorCode';
import { useLocalizationStore } from '@/store/localization-store';

/**
 * Ortega API 错误响应结构
 */
export interface OrtegaApiError {
    error: string;
    errorCode: number;
    category: string;
    ortegaAction: string;
}

/**
 * 获取错误消息的 stringKey
 */
function getErrorMessageKey(errorCode: number): string {
    return `[ErrorMessage${errorCode}]`;
}

/**
 * 获取错误码对应的枚举名称
 * @param errorCode 错误码数字
 * @returns 枚举名称或 undefined
 */
export function getErrorCodeName(errorCode: number): string | undefined {
    return ErrorCode[errorCode];
}

/**
 * 根据 errorCode 从 localization store 获取错误消息
 * 注意：此函数需要在 localization store 已加载后调用
 * @param errorCode 错误码
 * @returns 错误消息（如果未找到则返回 null）
 */
export function getErrorMessage(errorCode: number): string | null {
    const { resources } = useLocalizationStore.getState();
    if (!resources || Object.keys(resources).length === 0) {
        return null;
    }
    const key = getErrorMessageKey(errorCode);
    return resources[key] || null;
}

/**
 * 解析 Ortega API 错误
 * @param error 错误对象
 * @returns 解析后的错误信息
 */
export function parseOrtegaError(error: unknown): { message: string; code: number; raw: unknown } {
    const { t } = useLocalizationStore.getState();
    const result = {
        message: t('ERROR_GENERIC_RETRY'),
        code: 0,
        raw: error
    };

    // 处理 Axios 错误
    if (error instanceof AxiosError && error.response?.data) {
        const data = error.response.data as Partial<OrtegaApiError>;
        return parseOrtegaErrorData(data);
    }

    // 直接处理 Ortega API 错误数据
    if (error && typeof error === 'object' && 'errorCode' in error) {
        return parseOrtegaErrorData(error as Partial<OrtegaApiError>);
    }

    // 处理普通 Error 对象
    if (error instanceof Error) {
        result.message = error.message;
    }

    return result;
}

/**
 * 解析 Ortega API 错误数据
 */
function parseOrtegaErrorData(data: Partial<OrtegaApiError>): { message: string; code: number; raw: unknown } {
    const { t } = useLocalizationStore.getState();
    const result = {
        message: t('ERROR_GENERIC_RETRY'),
        code: data.errorCode || 0,
        raw: data
    };

    if (data.errorCode !== undefined) {
        // 优先从 localization store 获取错误消息
        const resourceMessage = getErrorMessage(data.errorCode);
        if (resourceMessage) {
            result.message = resourceMessage;
        } else {
            // 回退到枚举名称
            const codeName = getErrorCodeName(data.errorCode);
            result.message = codeName || t('ERROR_UNKNOWN_WITH_CODE', [data.errorCode]);
        }
    } else if (data.error) {
        result.message = data.error;
    }

    return result;
}
