import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { TooltipProvider } from '@/components/ui/tooltip';
import { BattleSimulationResult } from '@/api/generated/battleSimulationResult';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { useBattleReplay } from '@/hooks/useBattleReplay';
import { BattleCharacterAvatar } from './BattleCharacterAvatar';
import { BattleLogPanel } from './BattleLogPanel';
import { cn } from '@/lib/utils';
import {
    RotateCcw,
    ChevronLeft,
    ChevronRight,
    SkipBack,
    SkipForward,
    Swords
} from 'lucide-react';
import { useCallback, useEffect } from 'react';
import { useTranslation } from '@/hooks/useTranslation';

interface BattleReplayModalProps {
    isOpen: boolean;
    onClose: () => void;
    battleData: BattleSimulationResult | null;
}

/**
 * 伤害数字组件
 */
function DamageNumber({ value, isCrit }: { value: number; isCrit: boolean }) {
    const isHeal = value > 0;
    const displayValue = Math.abs(value).toLocaleString();
    
    return (
        <div 
            className={cn(
                "absolute font-bold text-lg animate-float-up pointer-events-none z-50",
                isHeal ? "text-green-500" : "text-red-500",
                isCrit && "text-yellow-500 text-xl"
            )}
            style={{
                animation: 'floatUp 1s ease-out forwards'
            }}
        >
            {isHeal ? '+' : '-'}{displayValue}
        </div>
    );
}

export function BattleReplayModal({
    isOpen,
    onClose,
    battleData
}: BattleReplayModalProps) {
    const { t } = useTranslation();
    const {
        currentIndex,
        totalEvents,
        currentTurn,
        characters,
        currentEvent,
        activeDamageEvents,
        snapshots,
        reset,
        goTo,
        next,
        prev,
        nextTurn,
        prevTurn
    } = useBattleReplay(battleData);

    // 关闭时重置
    const handleClose = useCallback(() => {
        reset();
        onClose();
    }, [reset, onClose]);

    // 键盘快捷键支持
    useEffect(() => {
        if (!isOpen) return;

        const handleKeyDown = (e: KeyboardEvent) => {
            if (e.key === 'ArrowLeft') {
                e.preventDefault();
                prev();
            } else if (e.key === 'ArrowRight') {
                e.preventDefault();
                next();
            }
        };

        window.addEventListener('keydown', handleKeyDown);
        return () => window.removeEventListener('keydown', handleKeyDown);
    }, [isOpen, prev, next]);

    // 点击日志事件跳转
    const handleEventClick = useCallback((index: number) => {
        goTo(index);
    }, [goTo]);

    if (!battleData) return null;

    // 分组角色
    const attackerChars = Array.from(characters.values())
        .filter(c => c.groupType === BattleFieldCharacterGroupType.Attacker);
    const defenderChars = Array.from(characters.values())
        .filter(c => c.groupType === BattleFieldCharacterGroupType.Receiver);

    // 获取当前行动角色（通过 sourceGuid 查找）
    const actingCharacter = currentEvent?.sourceGuid 
        ? Array.from(characters.values()).find(c => c.guid === currentEvent.sourceGuid)
        : null;

    return (
        <Dialog open={isOpen} onOpenChange={handleClose}>
            <DialogContent className="max-w-7xl max-h-[95vh] p-0 overflow-hidden">
                <TooltipProvider>
                <DialogHeader className="px-6 pr-14 pt-3 pb-3 border-b bg-muted/30">
                    <div className="flex items-center gap-6">
                        {/* 标题 */}
                        <div className="flex items-center gap-2 flex-shrink-0">
                            <Swords className="w-5 h-5 text-primary" />
                            <DialogTitle className="text-base">{t('BATTLE_REPLAY_TITLE')}</DialogTitle>
                        </div>

                        {/* 回合信息 */}
                        <div className="flex items-center gap-1 px-3 py-1 bg-background rounded-md border">
                            <Button
                                variant="ghost"
                                size="icon"
                                className="h-6 w-6"
                                onClick={prevTurn}
                                disabled={currentIndex <= 0}
                                title={t('BATTLE_REPLAY_PREV_TURN')}
                            >
                                <SkipBack className="w-3.5 h-3.5" />
                            </Button>
                            <span className="text-sm font-medium px-2">
                                {t('BATTLE_REPLAY_TURN', [String(currentTurn)])}
                            </span>
                            <Button
                                variant="ghost"
                                size="icon"
                                className="h-6 w-6"
                                onClick={nextTurn}
                                disabled={currentIndex >= totalEvents - 1}
                                title={t('BATTLE_REPLAY_NEXT_TURN')}
                            >
                                <SkipForward className="w-3.5 h-3.5" />
                            </Button>
                        </div>

                        {/* 当前事件 */}
                        {currentEvent && (
                            <span className="text-xs px-2 py-1 bg-primary/10 text-primary rounded">
                                {getEventLabel(currentEvent, t)}
                            </span>
                        )}

                        {/* 弹性间隔 */}
                        <div className="flex-1" />

                        {/* 步进控制 */}
                        <div className="flex items-center gap-1 px-3 py-1 bg-background rounded-md border">
                            <Button
                                variant="ghost"
                                size="icon"
                                onClick={prev}
                                disabled={currentIndex <= 0}
                                title={t('BATTLE_REPLAY_PREV_STEP')}
                                className="h-6 w-6"
                            >
                                <ChevronLeft className="w-4 h-4" />
                            </Button>
                            <span className="text-sm font-medium px-2 tabular-nums">
                                {currentIndex + 1} / {totalEvents}
                            </span>
                            <Button
                                variant="ghost"
                                size="icon"
                                onClick={next}
                                disabled={currentIndex >= totalEvents - 1}
                                title={t('BATTLE_REPLAY_NEXT_STEP')}
                                className="h-6 w-6"
                            >
                                <ChevronRight className="w-4 h-4" />
                            </Button>
                        </div>

                        {/* 重置按钮 */}
                        <Button
                            variant="outline"
                            size="icon"
                            onClick={reset}
                            title={t('BATTLE_REPLAY_RESET')}
                            className="h-8 w-8"
                        >
                            <RotateCcw className="w-4 h-4" />
                        </Button>
                    </div>
                </DialogHeader>

                {/* 战斗区域 */}
                <div className="flex gap-4 px-6 py-4 min-h-[400px]">
                    {/* 我方队伍 */}
                    <div className="w-[320px] space-y-2 flex-shrink-0">
                        <div className="text-xs font-medium text-blue-600 dark:text-blue-400 mb-2">
                            {t('BATTLE_REPLAY_ALLY_TEAM')}
                        </div>
                        {attackerChars.map(char => (
                            <BattleCharacterAvatar
                                key={char.id}
                                character={char}
                                isActing={actingCharacter?.guid === char.guid}
                            />
                        ))}
                    </div>

                    {/* 中央战斗日志区域 */}
                    <div className="flex-1 relative bg-gradient-to-b from-muted/30 to-muted/50 rounded-lg h-150 flex flex-col overflow-hidden">
                        {/* 标题栏 */}
                        <div className="px-3 py-2 border-b border-border/50 bg-muted/30 flex items-center justify-between">
                            <span className="text-xs font-medium text-muted-foreground">{t('BATTLE_REPLAY_BATTLE_LOG')}</span>
                            <span className="text-xs text-muted-foreground">
                                {t('BATTLE_REPLAY_TOTAL_RECORDS', [String(totalEvents)])}
                            </span>
                        </div>
                        
                        {/* 日志面板 */}
                        <BattleLogPanel
                            snapshots={snapshots}
                            currentIndex={currentIndex}
                            onEventClick={handleEventClick}
                        />

                        {/* 伤害数字浮动层 */}
                        <div className="absolute inset-0 pointer-events-none overflow-hidden">
                            {activeDamageEvents
                                .filter(event => event.value !== 0) // 过滤 Miss (value=0)
                                .map(event => {
                                    const targetChar = Array.from(characters.values()).find(
                                        c => c.guid === event.targetGuid && c.groupType === event.targetGroupType
                                    );
                                    if (!targetChar) return null;
                                    
                                    const isAttacker = targetChar.groupType === BattleFieldCharacterGroupType.Attacker;
                                    const positionClass = isAttacker ? 'left-[10%]' : 'right-[10%]';
                                    const offsetY = (event.id.charCodeAt(event.id.length - 1) % 40) - 20;
                                    
                                    return (
                                        <div
                                            key={event.id}
                                            className={cn("absolute top-1/2", positionClass)}
                                            style={{ 
                                                transform: `translateY(${-50 + offsetY}px)`,
                                                animation: 'floatUp 1s ease-out forwards'
                                            }}
                                        >
                                            <DamageNumber 
                                                value={event.value} 
                                                isCrit={event.isCrit} 
                                            />
                                        </div>
                                    );
                                })}
                        </div>
                    </div>

                    {/* 敌方队伍 */}
                    <div className="w-[320px] space-y-2 flex-shrink-0">
                        <div className="text-xs font-medium text-red-600 dark:text-red-400 mb-2 text-right">
                            {t('BATTLE_REPLAY_ENEMY_TEAM')}
                        </div>
                        {defenderChars.map(char => (
                            <BattleCharacterAvatar
                                key={char.id}
                                character={char}
                                isActing={actingCharacter?.guid === char.guid}
                            />
                        ))}
                    </div>
                </div>

                {/* 添加动画样式 */}
                <style>{`
                    @keyframes floatUp {
                        0% {
                            opacity: 1;
                            transform: translateY(0) scale(1);
                        }
                        100% {
                            opacity: 0;
                            transform: translateY(-50px) scale(0.8);
                        }
                    }
                    .animate-float-up {
                        animation: floatUp 1s ease-out forwards;
                    }
                `}</style>
                </TooltipProvider>
            </DialogContent>
        </Dialog>
    );
}

/**
 * 获取事件标签
 */
function getEventLabel(event: { type: string; skillId?: number }, t: (key: string, args?: string[]) => string): string {
    switch (event.type) {
        case 'turn_start':
            return t('BATTLE_REPLAY_TURN_START');
        case 'turn_end':
            return t('BATTLE_REPLAY_TURN_END');
        case 'action':
            return t('BATTLE_CHARACTER_SKILL', [String(event.skillId)]);
        case 'damage':
            return t('BATTLE_REPLAY_DAMAGE');
        case 'heal':
            return t('BATTLE_REPLAY_HEAL');
        case 'passive':
            return t('BATTLE_REPLAY_PASSIVE');
        case 'effect':
            return t('BATTLE_REPLAY_EFFECT');
        default:
            return event.type;
    }
}