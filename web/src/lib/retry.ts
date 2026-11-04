/**
 * 后端每次启动都会把账号重新登录一遍 (AutoLoginService), 这期间 user/getUserData 返回的还是空数据.
 * 首次进入页面时用这个轮询到真有数据为止, 不然要手动退出再进一次.
 */
export async function untilReady<T>(
    load: () => Promise<T>,
    ready: (value: T) => boolean,
    attempts = 60,
    intervalMs = 3000
): Promise<T | null> {
    for (let attempt = 0; attempt < attempts; attempt++) {
        try {
            const value = await load();
            if (ready(value)) return value;
        } catch {
            // 后端可能还在启动, 继续等
        }
        if (attempt < attempts - 1) await new Promise((resolve) => setTimeout(resolve, intervalMs));
    }
    return null;
}
