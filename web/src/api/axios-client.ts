import axios from 'axios';
import { useAccountStore } from '@/store/accountStore';
import { parseOrtegaError } from '@/lib/errorHandler';

const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_URL ?? 'http://localhost:5000',
    timeout: 30000,
    headers: {
        'Content-Type': 'application/json',
    },
});

// Request interceptor
apiClient.interceptors.request.use(
    (config) => {
        // 优先从请求数据中获取 userId (例如登录请求)
        let userId: string | null = null;
        
        if (config.data) {
            try {
                // 处理可能是 JSON 字符串或对象的情况
                const data = typeof config.data === 'string' ? JSON.parse(config.data) : config.data;
                if (data && data.userId) {
                    userId = data.userId.toString();
                }
            } catch {
                // 忽略解析错误
            }
        }
        
        // 如果请求数据中没有，则从 store 中获取当前账号 ID
        if (!userId) {
            const currentAccountId = useAccountStore.getState().currentAccountId;
            if (currentAccountId) {
                userId = currentAccountId.toString();
            }
        }

        if (userId) {
            config.headers['X-User-Id'] = userId;
        }

        // Keep existing auth token logic (if needed)
        const token = localStorage.getItem('auth_token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Response interceptor
apiClient.interceptors.response.use(
    (response) => response,
    (error) => {
        // Handle 401 - redirect to account management page
        if (error.response?.status === 401) {
            // Clear current account
            useAccountStore.getState().setCurrentAccount(null);
            // Redirect to accounts page if not already there
            if (!window.location.pathname.startsWith('/accounts')) {
                window.location.href = '/accounts';
            }
        }

        // 解析 Ortega API 错误
        if (error.response?.data?.errorCode !== undefined) {
            const parsedError = parseOrtegaError(error);
            // 将解析后的错误信息附加到 error 对象上
            error.ortegaError = parsedError;
        }

        return Promise.reject(error);
    }
);

export default apiClient;
