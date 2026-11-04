import { create } from 'zustand';
import { db } from '@/lib/db';
import { masterService, MasterTableManifest } from '@/api/master-service';
import { MasterBookBase } from '@/api/generated/masterBookBase';

interface MasterState {
    isInitializing: boolean;
    isSyncing: boolean;
    progress: { current: number; total: number };
    version: string | null;
    // 存储清单信息，用于按需同步时对比 hash
    manifestMap: Map<string, MasterTableManifest>;

    // 初始化：只获取清单，不下载数据
    sync: () => Promise<void>;

    // 获取单条数据 (按需同步 + 内存缓存)
    getRecord: <T extends MasterBookBase>(tableName: string, id: number) => Promise<T | null>;

    // 获取全表数据 (按需同步)
    getTable: <T extends MasterBookBase>(tableName: string) => Promise<T[]>;

    // 主动同步特定表（如果只想预加载某些表）
    syncTable: <T extends MasterBookBase>(tableName: string) => Promise<T[]>;
}

// 内存缓存以提高高频访问性能
const memoryCache: Record<string, MasterBookBase> = {};

// 同步锁，防止并发重复同步同一个表
const syncingLocks = new Map<string, Promise<void>>();

// 内部方法：检查并同步特定表
async function ensureTableSynced<T extends MasterBookBase>(
    tableName: string,
    manifestMap: Map<string, MasterTableManifest>
): Promise<void> {
    const tableManifest = manifestMap.get(tableName);

    if (!tableManifest) {
        console.warn(`[MasterStore] Table not found in manifest: ${tableName}`);
        return;
    }

    // 检查本地是否已是最新
    const localMeta = await db.meta.get(tableName);
    if (localMeta && localMeta.hash === tableManifest.hash) {
        return; // 已是最新，无需同步
    }

    // 检查是否有正在进行的同步
    const existingLock = syncingLocks.get(tableName);
    if (existingLock) {
        return existingLock; // 等待已有同步完成
    }

    // 创建新的同步锁
    const syncPromise = (async () => {
        console.log(`[MasterStore] Syncing table: ${tableName}...`);

        try {
            const data = await masterService.getTableData<T>(tableName);

            await db.transaction('rw', db.masterData, db.meta, async () => {
                await db.clearTable(tableName);
                await db.saveRecords(tableName, data);
                await db.meta.put({
                    tableName: tableName,
                    hash: tableManifest.hash,
                    lastUpdated: Date.now()
                });
            });

            // 清空该表的内存缓存（因为数据已更新）
            Object.keys(memoryCache).forEach(key => {
                if (key.startsWith(`${tableName}:`)) {
                    delete memoryCache[key];
                }
            });

            console.log(`[MasterStore] Table synced: ${tableName} (${data.length} records)`);
        } catch (error) {
            console.error(`[MasterStore] Failed to sync table ${tableName}:`, error);
            throw error;
        } finally {
            syncingLocks.delete(tableName);
        }
    })();

    syncingLocks.set(tableName, syncPromise);
    return syncPromise;
}

export const useMasterStore = create<MasterState>((set, get) => ({
    isInitializing: true,
    isSyncing: false,
    progress: { current: 0, total: 0 },
    version: null,
    manifestMap: new Map(),

    sync: async () => {
        set({ isSyncing: true });
        try {
            const manifest = await masterService.getManifest();
            const { tables, version } = manifest;

            // 只存储清单信息，不下载数据
            const manifestMap = new Map<string, MasterTableManifest>();
            for (const table of tables) {
                manifestMap.set(table.name, table);
            }

            set({
                version,
                manifestMap,
                progress: { current: 0, total: tables.length },
                isInitializing: false
            });
            console.log(`[MasterStore] Manifest loaded: ${tables.length} tables, version: ${version}`);
        } catch (error) {
            console.error('[MasterStore] Failed to load manifest:', error);
            set({ isInitializing: false });
        } finally {
            set({ isSyncing: false });
        }
    },

    getRecord: async <T extends MasterBookBase>(tableName: string, id: number): Promise<T | null> => {
        // 先尝试从内存缓存获取
        const cacheKey = `${tableName}:${id}`;
        if (memoryCache[cacheKey]) return memoryCache[cacheKey] as T;

        // 确保表已同步
        const { manifestMap } = get();
        if (manifestMap.size > 0) {
            await ensureTableSynced<T>(tableName, manifestMap);
        }

        // 从数据库获取
        const data = await db.getRecord<T>(tableName, id);
        if (data) {
            memoryCache[cacheKey] = data;
        }
        return data;
    },

    getTable: async <T extends MasterBookBase>(tableName: string): Promise<T[]> => {
        // 确保表已同步
        const { manifestMap } = get();
        if (manifestMap.size > 0) {
            await ensureTableSynced<T>(tableName, manifestMap);
        }

        return await db.getFullTable<T>(tableName);
    },

    syncTable: async <T extends MasterBookBase>(tableName: string): Promise<T[]> => {
        const { manifestMap } = get();
        if (manifestMap.size > 0) {
            await ensureTableSynced<T>(tableName, manifestMap);
        }
        return await db.getFullTable<T>(tableName);
    }
}));
