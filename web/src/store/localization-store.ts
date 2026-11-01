import { create } from 'zustand';
import { localizationApi } from '@/api/localization-service';
import { db } from '@/lib/db';
import { localTranslations } from '@/locales';

interface LocalizationState {
    currentLanguage: string;
    resources: Record<string, string>;
    isLoading: boolean;
    isSyncing: boolean;
    hash: string | null;
    lastUpdated: number | null;

    // Actions
    setLanguage: (lang: string) => Promise<void>;
    fetchResources: (lang: string) => Promise<void>;
    sync: (lang?: string) => Promise<void>;
    t: (key: string | undefined | null, params?: unknown[]) => string;
}

/**
 * 同一语言只跑一次 sync。
 * setLanguage() 和 App 挂载 effect 会同时触发 sync, 没有缓存时原本会并发下载两份 (~4.8MB)。
 */
const inFlightSyncs = new Map<string, Promise<void>>();

/**
 * 构造翻译函数。
 *
 * 每次语言或字典变化都会重新生成一个新引用, 和 react-i18next 的做法一致:
 * 组件只要订阅了 `t` (useLocalizationStore(s => s.t) / useTranslation()), 就会随之重渲染;
 * useMemo / useEffect 依赖 `t` 也会自动失效, 不用到处补 language。
 *
 * 函数内部仍读最新的 get(), 所以旧引用被调用时返回的也是当前语言。
 */
const buildT = (get: () => LocalizationState) =>
    (key: string | undefined | null, params?: unknown[]): string => {
        if (!key) return '';
        const { resources, currentLanguage } = get();

        // 如果资源未加载或找不到 key，尝试本地翻译，如果还找不到返回 key
        let text = resources?.[key] ?? localTranslations[currentLanguage]?.[key] ?? localTranslations.enUS?.[key] ?? key;

        // 处理 C# 风格的占位符 {0}, {0:D2}, etc.
        if (params && params.length > 0) {
            params.forEach((param, index) => {
                // 1. 处理带格式的占位符，如 {0:D2}
                const formattedRegex = new RegExp(`\\{${index}:([^}]+)\\}`, 'g');
                text = text.replace(formattedRegex, (_, format) => {
                    const value = String(param);
                    if (format.startsWith('D')) {
                        const digits = parseInt(format.substring(1));
                        if (!isNaN(digits)) {
                            return value.padStart(digits, '0');
                        }
                    }
                    return value;
                });

                // 2. 处理基本占位符 {0}
                const basicPlaceholder = `{${index}}`;
                text = text.replaceAll(basicPlaceholder, String(param));
            });
        }

        return text;
    };

export const useLocalizationStore = create<LocalizationState>((set, get) => ({
    currentLanguage: localStorage.getItem('app_lang') || 'enUS',
    resources: {},
    isLoading: false,
    isSyncing: false,
    hash: null,
    lastUpdated: null,
    t: buildT(get),

    setLanguage: async (lang: string) => {
        localStorage.setItem('app_lang', lang);
        // t 换新引用 -> 所有订阅 t 的组件重渲染, 依赖 t 的 memo/effect 重算
        set({ currentLanguage: lang, t: buildT(get) });
        await get().sync(lang);
    },

    fetchResources: async (lang: string) => {
        // 如果已经有资源且语言匹配，且不是强制刷新，可以跳过 (这里简化为先查 DB)
        set({ isLoading: true });
        try {
            // 1. 尝试从 IndexedDB 获取
            const cached = await db.getTranslationRecord(lang);
            if (cached && Object.keys(cached.resources).length > 0) {
                // 读库期间可能又切了语言, 只有仍是当前语言才写回
                if (get().currentLanguage === lang) {
                    set({
                        resources: cached.resources,
                        hash: cached.hash,
                        lastUpdated: cached.lastUpdated,
                        t: buildT(get)
                    });
                }
                set({ isLoading: false });

                // 后台静默检查更新
                get().sync(lang);
                return;
            }

            // 2. 如果没有缓存，从 API 获取
            await get().sync(lang);
        } catch (error) {
            console.error('Failed to fetch localization resources:', error);
        } finally {
            set({ isLoading: false });
        }
    },

    sync: async (lang?: string) => {
        const targetLang = lang || get().currentLanguage;

        const running = inFlightSyncs.get(targetLang);
        if (running) return running;

        const task = (async () => {
            set({ isSyncing: true });

            try {
                // 1. 获取服务器 manifest
                const manifest = await localizationApi.getManifest(targetLang);

                // 2. 获取本地缓存
                const localRecord = await db.translations.get(targetLang);

                // 3. 如果 hash 不一致或本地无数据，则同步
                if (!localRecord || localRecord.hash !== manifest.hash) {
                    console.log(`Syncing localization resources for: ${targetLang}...`);
                    const resources = await localizationApi.getResources(targetLang);

                    // 4. 保存到 IndexedDB
                    await db.saveTranslations(targetLang, resources, manifest.hash);

                    // 5. 更新状态: 只有它仍是当前语言才写回。
                    //    否则先发出的旧语言请求会在晚到后覆盖新语言 (切太快的根因)
                    if (get().currentLanguage === targetLang) {
                        set({
                            resources,
                            hash: manifest.hash,
                            lastUpdated: manifest.lastUpdated,
                            t: buildT(get)
                        });
                    }
                } else if (get().currentLanguage === targetLang && localRecord.resources
                    && Object.keys(localRecord.resources).length > 0) {
                    // hash 相同说明缓存就是最新的, 直接回填内存, 不必再走网络
                    set({
                        resources: localRecord.resources,
                        hash: localRecord.hash,
                        lastUpdated: localRecord.lastUpdated,
                        t: buildT(get)
                    });
                }
            } catch (error) {
                console.error('Localization sync failed:', error);
            } finally {
                inFlightSyncs.delete(targetLang);
                set({ isSyncing: false });
            }
        })();

        inFlightSyncs.set(targetLang, task);
        return task;
    },
}));
