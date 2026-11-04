import apiClient from './axios-client';
import { useAccountStore } from '@/store/accountStore';
import type { UserSyncData } from '@/api/generated/userSyncData';

import type { OrtegaRpcMap } from './ortega-rpc-manifest';

/**
 * 强类型 JSON-RPC 客户端基类
 */
export const rpcClient = {
    /**
     * 调用 Ortega API
     * @param uri 路由地址
     * @param request 请求载荷
     * @returns 响应数据
     */
    async call<TResponse>(
        uri: string,
        request: unknown
    ): Promise<TResponse> {
        const [category, action] = uri.split('/');

        // 保持目前的 API 代理格式: /api/ortega/{category}/{action}
        // 如果后端改为真正的单一入口 JSON-RPC，只需在此处修改 URL 和 Body 结构
        const response = await apiClient.post<TResponse>(
            `/api/ortega/${category.trim()}/${action.trim()}`,
            request
        );

        const data = response.data as TResponse;
        const userSyncData = (data as { userSyncData?: UserSyncData | null }).userSyncData;
        if (userSyncData) {
            const currentAccountId = useAccountStore.getState().currentAccountId;
            if (currentAccountId) {
                useAccountStore.getState().mergeUserSyncData(currentAccountId, userSyncData);
            }
        }

        return data;
    },

    /**
     * 批量调用 (JSON-RPC 核心优势)
     * 后端支持后，可在此处实现将多个请求合并为一个 POST /api/rpc 数组
     */
    async batch(requests: { [K in keyof OrtegaRpcMap]?: OrtegaRpcMap[K]['request'] }) {
        console.warn('Batch mode requires backend support for /api/rpc', requests);
    }
};

export default rpcClient;
