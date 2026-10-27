import Dexie, { Table } from 'dexie';
import { MasterBookBase } from '@/api/generated/masterBookBase';

export interface MasterMeta {
    tableName: string;
    hash: string;
    lastUpdated: number;
}

export interface MasterRecord {
    table_id: string; // 复合主键: tableName + ":" + id
    tableName: string;
    id: number;
    data: unknown;
}

export interface TranslationRecord {
    lang: string;
    resources: Record<string, string>;
    hash: string;
    lastUpdated: number;
}

export class MementoMoriDB extends Dexie {
    meta!: Table<MasterMeta>;
    masterData!: Table<MasterRecord>;
    translations!: Table<TranslationRecord>;

    constructor() {
        super('MementoMoriDB');
        this.version(2).stores({
            meta: 'tableName',
            masterData: 'table_id, tableName, id',
            translations: 'lang'
        });
    }

    /**
     * 保存翻译资源
     */
    async saveTranslations(lang: string, resources: Record<string, string>, hash: string) {
        await this.translations.put({
            lang,
            resources,
            hash,
            lastUpdated: Date.now()
        });
    }

    /**
     * 获取翻译资源
     */
    async getTranslations(lang: string): Promise<Record<string, string> | null> {
        const record = await this.translations.get(lang);
        return record ? record.resources : null;
    }

    /**
     * 获取翻译资源记录（包含 hash 等元数据）
     */
    async getTranslationRecord(lang: string): Promise<TranslationRecord | null> {
        return await this.translations.get(lang) || null;
    }

    /**
     * 清除特定表的数据
     */
    async clearTable(tableName: string) {
        await this.masterData.where('tableName').equals(tableName).delete();
    }

    /**
     * 批量保存记录
     */
    async saveRecords<T extends MasterBookBase>(tableName: string, records: T[]) {
        const masterRecords: MasterRecord[] = records.map(r => ({
            table_id: `${tableName}:${r.id}`,
            tableName,
            id: r.id,
            data: r
        }));
        await this.masterData.bulkPut(masterRecords);
    }

    /**
     * 获取单条记录
     */
    async getRecord<T extends MasterBookBase>(tableName: string, id: number): Promise<T | null> {
        const record = await this.masterData.get(`${tableName}:${id}`);
        return record ? record.data as T : null;
    }

    /**
     * 获取全表数据
     */
    async getFullTable<T extends MasterBookBase>(tableName: string): Promise<T[]> {
        const records = await this.masterData.where('tableName').equals(tableName).toArray();
        return records.map(r => r.data as T);
    }
}

export const db = new MementoMoriDB();
