import { useState, useEffect } from 'react';
import { useMasterStore } from '@/store/masterStore';
import { MasterBookBase } from '@/api/generated/masterBookBase';

interface MasterState {
    getRecord: <T extends MasterBookBase>(tableName: string, id: number) => Promise<T | null>;
    getTable: <T extends MasterBookBase>(tableName: string) => Promise<T[]>;
}

/**
 * 通用 Hook 用于获取 Master 数据
 * @param tableName 表名，如 'CharacterTable'
 * @param id 记录 ID
 * @returns { data: T | null, loading: boolean }
 */
export function useMasterData<T extends MasterBookBase>(tableName: string, id: number | string | undefined) {
    const [data, setData] = useState<T | null>(null);
    const [loading, setLoading] = useState(true);
    const getRecord = useMasterStore((state: MasterState) => state.getRecord);

    useEffect(() => {
        if (id === undefined || id === null) {
            setData(null);
            setLoading(false);
            return;
        }

        let isMounted = true;
        setLoading(true);

        const numericId = typeof id === 'string' ? parseInt(id, 10) : id;
        
        getRecord<T>(tableName, numericId).then(res => {
            if (isMounted) {
                setData(res);
                setLoading(false);
            }
        }).catch(() => {
            if (isMounted) {
                setData(null);
                setLoading(false);
            }
        });

        return () => {
            isMounted = false;
        };
    }, [tableName, id, getRecord]);

    return { data, loading };
}

/**
 * Hook 用于获取 Master 全表数据
 * @param tableName 表名
 * @returns { data: T[] | null, loading: boolean }
 */
export function useMasterTable<T extends MasterBookBase>(tableName: string) {
    const [data, setData] = useState<T[] | null>(null);
    const [loading, setLoading] = useState(true);
    const getTable = useMasterStore((state: MasterState) => state.getTable);

    useEffect(() => {
        let isMounted = true;
        setLoading(true);

        getTable<T>(tableName).then(res => {
            if (isMounted) {
                setData(res);
                setLoading(false);
            }
        }).catch(() => {
            if (isMounted) {
                setData(null);
                setLoading(false);
            }
        });

        return () => {
            isMounted = false;
        };
    }, [tableName, getTable]);

    return { data, loading };
}
