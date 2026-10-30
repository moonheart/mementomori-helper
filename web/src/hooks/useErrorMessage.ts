import { useCallback } from 'react';
import { useTranslation } from './useTranslation';
import { ErrorCode } from '@/api/generated/errorCode';

interface OrtegaApiError {
    error: string;
    errorCode: number;
    category: string;
    ortegaAction: string;
}

/**
 * 根据 errorCode 获取错误消息的 stringKey
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
 * Hook 用于获取错误消息
 */
export function useErrorMessage() {
    const { t, loading } = useTranslation();

    /**
     * 根据 errorCode 获取错误消息文本
     */
    const getErrorMessage = useCallback((errorCode: number): string | null => {
        const key = getErrorMessageKey(errorCode);
        const message = t(key);
        // 如果翻译返回的是 key 本身，说明没有找到对应的消息
        return message === key ? null : message;
    }, [t]);

    /**
     * 解析 Ortega API 错误并返回友好的错误消息
     */
    const parseOrtegaError = useCallback((error: unknown): { message: string; code: number } => {
        // 默认错误消息
        let message = t('ERROR_GENERIC_RETRY');
        let code = 0;

        if (error && typeof error === 'object') {
            const err = error as Partial<OrtegaApiError>;

            if (err.errorCode !== undefined) {
                code = err.errorCode;
                // 尝试从翻译资源中获取错误消息
                const resourceMessage = getErrorMessage(code);
                if (resourceMessage) {
                    message = resourceMessage;
                } else {
                    // 回退到枚举名称
                    const codeName = getErrorCodeName(code);
                    message = codeName || t('ERROR_UNKNOWN_WITH_CODE', [code]);
                }
            } else if (err.error) {
                message = err.error;
            }
        } else if (error instanceof Error) {
            message = error.message;
        }

        return { message, code };
    }, [getErrorMessage, t]);

    return {
        getErrorMessage,
        parseOrtegaError,
        isLoading: loading,
        getErrorCodeName
    };
}
