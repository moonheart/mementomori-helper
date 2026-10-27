import apiClient from './axios-client';
import type {
    AccountDto,
    AddAccountWithClientKeyRequest,
    AddAccountWithPasswordRequest,
    GetClientKeyResponse,
} from './generated';

/** 登录请求参数 */
export interface LoginRequest {
    userId: number;
    clientKey: string;
}

/** 登录响应 */
export interface LoginResult {
    success: boolean;
    message: string;
}

export const accountApi = {
    /**
     * 获取所有账号
     */
    getAccounts: async (): Promise<AccountDto[]> => {
        const response = await apiClient.get<AccountDto[]>('/api/auth/accounts');
        return response.data;
    },

    /**
     * 添加账号 - 方式1：使用 ClientKey
     */
    addAccountWithClientKey: async (
        request: AddAccountWithClientKeyRequest
    ): Promise<AccountDto> => {
        const response = await apiClient.post<AccountDto>(
            '/api/auth/accounts/with-clientkey',
            request
        );
        return response.data;
    },

    /**
     * 添加账号 - 方式2：使用密码（引继码）
     */
    addAccountWithPassword: async (
        request: AddAccountWithPasswordRequest
    ): Promise<GetClientKeyResponse> => {
        const response = await apiClient.post<GetClientKeyResponse>(
            '/api/auth/accounts/with-password',
            request
        );
        return response.data;
    },

    /**
     * 删除账号
     */
    deleteAccount: async (userId: number): Promise<void> => {
        await apiClient.delete(`/api/auth/accounts/${userId}`);
    },

    /**
     * 登录到游戏
     */
    login: async (request: LoginRequest): Promise<LoginResult> => {
        const response = await apiClient.post<LoginResult>(
            '/api/auth/login',
            request
        );
        return response.data;
    },
};
