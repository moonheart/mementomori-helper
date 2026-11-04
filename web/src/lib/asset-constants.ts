import { API_BASE } from '@/lib/base-path';

// 游戏图片走 API 的本地缓存 (/api/assets), 第一次下载后存在服务器磁盘, 浏览器缓存一年
const API_URL = API_BASE;

/** 资源服务器基础URL (AddressableConvertAssets) */
export const BASE_URL = `${API_URL}/api/assets/convert`;

/** 本地资源服务器基础URL (AddressableLocalAssets) */
export const BASE_URL_LOCAL = `${API_URL}/api/assets/local`;
