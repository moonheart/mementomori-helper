/**
 * 运行时 base path。
 *
 * nginx 通过 sub_filter 把 index.html 里的 <base href="/"> 改写成 <base href="/5290/">,
 * 这里直接读 document.baseURI, 因此反代前缀变化时不需要重新构建前端。
 */
export const BASE_PATH = new URL(document.baseURI).pathname.replace(/\/+$/, "");

/**
 * API / SignalR / 资源地址的前缀。
 * 如果显式配置了 VITE_API_URL (例如独立 API 域名), 优先使用它。
 */
export const API_BASE = import.meta.env.VITE_API_URL || BASE_PATH;
