import { useEffect, useRef } from 'react';
import { Card, CardHeader, CardTitle, CardContent } from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Badge } from '@/components/ui/badge';
import { Terminal, X, Minus, Maximize2, Trash2 } from 'lucide-react';
import { useLogStore } from '@/store/logStore';
import { useAccountStore } from '@/store/accountStore';
import * as signalR from '@microsoft/signalr';
import { cn } from '@/lib/utils';
import { useTranslation } from '@/hooks/useTranslation';
import { API_BASE } from '@/lib/base-path';

export function GlobalLogConsole() {
    const { t } = useTranslation();
    const {
        logs,
        isConnected,
        viewState,
        addLog,
        clearLogs,
        setIsConnected,
        setViewState
    } = useLogStore();

    const currentAccountId = useAccountStore((state) => state.currentAccountId);
    const scrollRef = useRef<HTMLDivElement>(null);
    const connectionRef = useRef<signalR.HubConnection | null>(null);
    const userClosedRef = useRef(false);
    const prevLogsLengthRef = useRef(0);

    // 监听新日志，如果是 collapsed 状态且用户未手动关闭，则自动展开
    useEffect(() => {
        if (logs.length > prevLogsLengthRef.current) {
            // 有新日志
            if (viewState === 'collapsed' && !userClosedRef.current) {
                setViewState('compact');
            }
        }
        prevLogsLengthRef.current = logs.length;
    }, [logs, viewState, setViewState]);

    // SignalR 连接
    useEffect(() => {
        if (!currentAccountId) {
            setIsConnected(false);
            return;
        }

        const connection = new signalR.HubConnectionBuilder()
            .withUrl(`${API_BASE}/hubs/jobs?userId=${currentAccountId}`)
            .withAutomaticReconnect()
            .build();

        connectionRef.current = connection;

        connection.on('ReceiveLog', (log) => {
            addLog(log);
        });

        const start = async () => {
            try {
                await connection.start();
                setIsConnected(true);
                console.log('SignalR connected for user:', currentAccountId);
            } catch (err) {
                console.error('SignalR connection failed: ', err);
                setIsConnected(false);
            }
        };

        start();

        return () => {
            connection.stop();
        };
    }, [currentAccountId, addLog, setIsConnected]);

    // 自动滚动到底部
    useEffect(() => {
        if (scrollRef.current && (viewState === 'expanded' || viewState === 'compact')) {
            scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
        }
    }, [logs, viewState]);

    // 获取最新的3条日志（用于 compact 模式）
    const recentLogs = logs.slice(-3);

    // 收起状态 - 悬浮小球
    if (viewState === 'collapsed') {
        return (
            <button
                onClick={() => {
                    userClosedRef.current = false;
                    setViewState('compact');
                }}
                className={cn(
                    "fixed bottom-6 right-6 z-50 w-12 h-12 rounded-full shadow-lg",
                    "flex items-center justify-center",
                    "transition-all duration-200 hover:scale-110",
                    isConnected
                        ? "bg-green-500 hover:bg-green-600 text-white"
                        : "bg-zinc-700 hover:bg-zinc-600 text-zinc-300"
                )}
                title={t('GLOBAL_LOG_VIEW_LOGS')}
            >
                <Terminal className="h-5 w-5" />
                {logs.length > 0 && (
                    <span className="absolute -top-1 -right-1 w-5 h-5 bg-red-500 text-white text-[10px] rounded-full flex items-center justify-center">
                        {logs.length > 99 ? '99+' : logs.length}
                    </span>
                )}
            </button>
        );
    }

    // 紧凑状态 - 显示最近3条
    if (viewState === 'compact') {
        return (
            <Card className="fixed bottom-6 right-6 z-50 w-80 shadow-xl border-zinc-700">
                <CardHeader className="flex flex-row items-center justify-between space-y-0 py-2 px-3 bg-zinc-900 rounded-t-lg">
                    <div className="flex items-center gap-2">
                        <Terminal className="h-3.5 w-3.5 text-zinc-400" />
                        <CardTitle className="text-xs font-medium text-zinc-300">{t('GLOBAL_LOG_TITLE')}</CardTitle>
                        <div className={cn(
                            "w-2 h-2 rounded-full",
                            isConnected ? "bg-green-500" : "bg-red-500"
                        )} />
                    </div>
                    <div className="flex items-center gap-1">
                        <Button 
                            variant="ghost" 
                            size="icon" 
                            className="h-6 w-6 text-zinc-500 hover:text-zinc-300"
                            onClick={() => setViewState('expanded')}
                        >
                            <Maximize2 className="h-3 w-3" />
                        </Button>
                        <Button
                            variant="ghost"
                            size="icon"
                            className="h-6 w-6 text-zinc-500 hover:text-zinc-300"
                            onClick={() => {
                                userClosedRef.current = true;
                                setViewState('collapsed');
                            }}
                        >
                            <Minus className="h-3 w-3" />
                        </Button>
                    </div>
                </CardHeader>
                <CardContent
                    ref={scrollRef}
                    className="bg-black p-2 font-mono text-[10px] max-h-32 overflow-y-auto"
                >
                    {recentLogs.length === 0 ? (
                        <p className="text-zinc-600 italic py-2 text-center">{t('GLOBAL_LOG_NO_LOGS')}</p>
                    ) : (
                        <div className="space-y-1">
                            {recentLogs.map((log, i) => (
                                <div key={i} className="flex gap-1.5 truncate">
                                    <span className="text-zinc-600 shrink-0">[{log.time.split(' ')[1]}]</span>
                                    <span className={cn(
                                        "truncate",
                                        log.level === 'Error' ? 'text-red-400' : 
                                        log.level === 'Warning' ? 'text-yellow-400' : 
                                        'text-zinc-300'
                                    )}>
                                        {log.message}
                                    </span>
                                </div>
                            ))}
                        </div>
                    )}
                </CardContent>
            </Card>
        );
    }

    // 展开状态 - 完整日志
    return (
        <Card className="fixed bottom-6 right-6 z-50 w-[600px] h-[400px] shadow-2xl border-zinc-700 flex flex-col">
            <CardHeader className="flex flex-row items-center justify-between space-y-0 py-2 px-4 bg-zinc-900 rounded-t-lg shrink-0">
                <div className="flex items-center gap-2">
                    <Terminal className="h-4 w-4 text-zinc-400" />
                    <CardTitle className="text-sm font-medium text-zinc-300">{t('GLOBAL_LOG_TITLE')}</CardTitle>
                    {isConnected ? (
                        <Badge variant="secondary" className="bg-green-900/50 text-green-400 hover:bg-green-900/50 text-[10px] border-0">
                            {t('GLOBAL_LOG_CONNECTED')}
                        </Badge>
                    ) : (
                        <Badge variant="destructive" className="text-[10px] bg-red-900/50 text-red-400 border-0">
                            {t('GLOBAL_LOG_DISCONNECTED')}
                        </Badge>
                    )}
                </div>
                <div className="flex items-center gap-1">
                    <Button
                        variant="ghost"
                        size="sm"
                        className="h-7 px-2 text-xs text-zinc-500 hover:text-red-400"
                        onClick={clearLogs}
                    >
                        <Trash2 className="h-3 w-3 mr-1" />
                        {t('GLOBAL_LOG_CLEAR')}
                    </Button>
                    <Button 
                        variant="ghost" 
                        size="icon" 
                        className="h-7 w-7 text-zinc-500 hover:text-zinc-300"
                        onClick={() => setViewState('compact')}
                    >
                        <Minus className="h-3.5 w-3.5" />
                    </Button>
                    <Button
                        variant="ghost"
                        size="icon"
                        className="h-7 w-7 text-zinc-500 hover:text-zinc-300"
                        onClick={() => {
                            userClosedRef.current = true;
                            setViewState('collapsed');
                        }}
                    >
                        <X className="h-3.5 w-3.5" />
                    </Button>
                </div>
            </CardHeader>
            <CardContent 
                ref={scrollRef}
                className="flex-1 overflow-auto bg-black rounded-b-lg p-3 font-mono text-[11px]"
            >
                <div className="space-y-1">
                    {logs.length === 0 ? (
                        <p className="text-zinc-600 italic text-center py-8">{t('GLOBAL_LOG_WAITING')}</p>
                    ) : (
                        logs.map((log, i) => (
                            <div key={i} className="flex gap-2">
                                <span className="text-zinc-600 shrink-0">[{log.time.split(' ')[1]}]</span>
                                <span className={cn(
                                    log.level === 'Error' ? 'text-red-400' : 
                                    log.level === 'Warning' ? 'text-yellow-400' : 
                                    'text-zinc-300'
                                )}>
                                    {log.message}
                                </span>
                            </div>
                        ))
                    )}
                </div>
            </CardContent>
        </Card>
    );
}
