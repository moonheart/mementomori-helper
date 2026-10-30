import { getTimeManager, TimeManager } from '@/lib/time-manager';
import { useAccountStore } from '@/store/accountStore';

/**
 * 返回当前账户对应的 TimeManager 实例。
 * 若尚未选中账户，则返回 userId=0 的默认实例（偏移量为 0）。
 */
export function useTimeManager(): TimeManager {
    const currentAccountId = useAccountStore((state) => state.currentAccountId);
    return getTimeManager(currentAccountId ?? 0);
}
