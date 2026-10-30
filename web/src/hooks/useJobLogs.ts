import { useEffect, useState, useCallback, useRef } from 'react';
import * as signalR from '@microsoft/signalr';

export interface JobLog {
    time: string;
    level: string;
    message: string;
}

/**
 * SignalR Job Log Hook
 * @param userId 账户 ID
 */
export function useJobLogs(userId: number | null) {
    const [logs, setLogs] = useState<JobLog[]>([]);
    const [isConnected, setIsConnected] = useState(false);
    const userIdRef = useRef(userId);

    // 同步 userIdRef 以便在 effect 内部使用
    useEffect(() => {
        userIdRef.current = userId;
    }, [userId]);

    useEffect(() => {
        // 只有当 userId 存在时才开始连接
        if (!userId) {
            return;
        }

        const connection = new signalR.HubConnectionBuilder()
            .withUrl(`${import.meta.env.VITE_API_URL ?? 'http://localhost:5000'}/hubs/jobs?userId=${userId}`)
            .withAutomaticReconnect()
            .build();

        connection.on('ReceiveLog', (log: JobLog) => {
            setLogs(prev => [log, ...prev].slice(0, 100)); // 保留最近 100 条
        });

        const start = async () => {
            try {
                await connection.start();
                setIsConnected(true);
                console.log('SignalR connected for jobs');
            } catch (err) {
                console.error('SignalR connection failed: ', err);
                setIsConnected(false);
            }
        };

        start().then(() => {
            setLogs([]); // 连接成功后清空旧日志
        });

        return () => {
            connection.stop();
        };
    }, [userId]);


    const clearLogs = useCallback(() => {
        setLogs([]);
    }, []);

    return { logs, isConnected, clearLogs };
}
