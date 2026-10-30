import axios from 'axios';

// 从环境变量获取 API URL，默认为 localhost:5000
const baseURL = import.meta.env.VITE_API_URL ?? 'http://localhost:5000';

export const apiClient = axios.create({
    baseURL,
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 30000, // 30 seconds
});

// 请求拦截器
apiClient.interceptors.request.use(
    (config) => {
        // 可以在这里添加认证 token 等
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// 响应拦截器
apiClient.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        // 统一错误处理
        if (error.response) {
            // 服务器返回错误状态码
            console.error('API Error:', error.response.status, error.response.data);
        } else if (error.request) {
            // 请求已发出但没有收到响应
            console.error('Network Error:', error.message);
        } else {
            // 其他错误
            console.error('Error:', error.message);
        }
        return Promise.reject(error);
    }
);
