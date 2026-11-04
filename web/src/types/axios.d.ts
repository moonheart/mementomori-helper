import 'axios';

declare module 'axios' {
    export interface AxiosError {
        /**
         * 解析后的 Ortega API 错误信息
         * 仅在响应包含 errorCode 时存在
         */
        ortegaError?: {
            message: string;
            code: number;
            raw: unknown;
        };
    }
}
