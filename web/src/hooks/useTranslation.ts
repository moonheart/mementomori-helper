import { useCallback } from 'react';
import { useLocalizationStore } from '@/store/localization-store';

/**
 * useTranslation Hook
 * 统一封装 useLocalizationStore，为现有组件提供兼容接口
 */
export function useTranslation() {
    const t = useLocalizationStore(state => state.t);
    const language = useLocalizationStore(state => state.currentLanguage);
    const loading = useLocalizationStore(state => state.isLoading);
    const setLanguage = useLocalizationStore(state => state.setLanguage);

    const translate = useCallback((key: string | undefined | null, params?: unknown[]) => t(key, params), [t]);

    return {
        t: translate,
        language,
        loading,
        setLanguage
    };
}
