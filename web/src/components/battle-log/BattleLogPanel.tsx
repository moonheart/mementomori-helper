import { useEffect, useRef, useCallback, useMemo } from 'react';
import { BattleStateSnapshot } from '@/hooks/useBattleReplay';
import { BattleLogEntry } from './BattleLogEntry';
import { cn } from '@/lib/utils';
import { useTranslation } from '@/hooks/useTranslation';

interface BattleLogPanelProps {
    snapshots: BattleStateSnapshot[];
    currentIndex: number;
    onEventClick?: (index: number) => void;
    className?: string;
}

/**
 * 战斗日志面板组件 - 状态机版本
 * 显示完整的战斗日志，自动滚动到当前播放位置
 */
export function BattleLogPanel({
    snapshots,
    currentIndex,
    onEventClick,
    className
}: BattleLogPanelProps) {
    const { t } = useTranslation();
    const containerRef = useRef<HTMLDivElement>(null);
    const currentEventRef = useRef<HTMLDivElement>(null);
    
    // 按回合分组快照
    const groupedSnapshots = useMemo(() => {
        const grouped = new Map<number, BattleStateSnapshot[]>();
        snapshots.forEach(snapshot => {
            const turn = snapshot.turn;
            if (!grouped.has(turn)) {
                grouped.set(turn, []);
            }
            grouped.get(turn)!.push(snapshot);
        });
        return grouped;
    }, [snapshots]);
    
    // 自动滚动到当前事件
    useEffect(() => {
        if (currentEventRef.current && containerRef.current) {
            const container = containerRef.current;
            const targetElement = currentEventRef.current;
            
            const containerHeight = container.clientHeight;
            const targetTop = targetElement.offsetTop;
            const targetHeight = targetElement.clientHeight;
            const scrollPosition = targetTop - (containerHeight / 2) + (targetHeight / 2);
            
            container.scrollTo({
                top: Math.max(0, scrollPosition),
                behavior: 'smooth'
            });
        }
    }, [currentIndex]);
    
    // 判断事件状态
    const getEventStatus = useCallback((index: number) => {
        if (index === currentIndex) return 'current';
        if (index < currentIndex) return 'past';
        return 'future';
    }, [currentIndex]);
    
    return (
        <div 
            ref={containerRef}
            className={cn(
                "flex-1 overflow-y-auto",
                "[&::-webkit-scrollbar]:w-2.5",
                "[&::-webkit-scrollbar-track]:bg-transparent",
                "[&::-webkit-scrollbar-thumb]:bg-slate-400 dark:bg-slate-500",
                "[&::-webkit-scrollbar-thumb]:rounded-full",
                "[&::-webkit-scrollbar-thumb:hover]:bg-slate-500 dark:bg-slate-400",
                className
            )}
        >
            <div className="py-2 px-1">
                {Array.from(groupedSnapshots.entries()).map(([turn, turnSnapshots]) => (
                    <div key={turn} className="mb-2">
                        {/* 回合分隔标题 */}
                        <div className="sticky top-0 z-10 bg-gradient-to-b from-muted via-muted to-transparent pb-2 pt-1">
                            <div className="flex items-center gap-2 px-2">
                                <div className="h-px flex-1 bg-border" />
                                <span className="text-xs font-medium text-muted-foreground">
                                    {t('BATTLE_LOG_PANEL_TURN', [String(turn)])}
                                </span>
                                <div className="h-px flex-1 bg-border" />
                            </div>
                        </div>
                        
                        {/* 该回合的事件列表 */}
                        <div className="space-y-0.5">
                            {turnSnapshots.map((snapshot) => {
                                const status = getEventStatus(snapshot.index);
                                const isCurrent = status === 'current';
                                const isPast = status === 'past' || status === 'current';
                                
                                return (
                                    <BattleLogEntry
                                        key={snapshot.event.id}
                                        ref={isCurrent ? currentEventRef : null}
                                        event={snapshot.event}
                                        characters={snapshot.characters}
                                        isCurrent={isCurrent}
                                        isPast={isPast}
                                        onClick={() => onEventClick?.(snapshot.index)}
                                    />
                                );
                            })}
                        </div>
                    </div>
                ))}
                
                {/* 空状态 */}
                {snapshots.length === 0 && (
                    <div className="flex items-center justify-center h-32 text-muted-foreground">
                        {t('BATTLE_LOG_PANEL_EMPTY')}
                    </div>
                )}
            </div>
        </div>
    );
}