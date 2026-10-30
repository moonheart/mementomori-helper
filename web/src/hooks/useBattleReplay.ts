import { useState, useCallback, useRef, useEffect, useMemo } from 'react';
import { BattleSimulationResult } from '@/api/generated/battleSimulationResult';
import { SubSkillResult } from '@/api/generated/subSkillResult';
import { SubSkillResultType } from '@/api/generated/subSkillResultType';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { HitType } from '@/api/generated/hitType';
import { EffectGroup } from '@/api/generated/effectGroup';
import { SkillDisplayType } from '@/api/generated/skillDisplayType';
import { SkillCategory } from '@/api/generated/skillCategory';
import { EffectType } from '@/api/generated/effectType';
import { useTranslation } from '@/hooks/useTranslation';

/** 重播状态机状态 */
export type ReplayState = 'idle' | 'playing' | 'paused' | 'ended';

/** 活跃效果 */
export interface ActiveEffect {
    effectType: EffectType;
    effectValue: number;
    effectCount: number;
}

/** 活跃效果组 */
export interface ActiveEffectGroup {
    effectGroupId: number;
    skillCategory: SkillCategory;
    effectTurn: number;
    effects: ActiveEffect[];
    granterGuid: number;
}

/** 角色状态 */
export interface CharacterState {
    id: string;  // 全局唯一ID: `${groupType}-${battleCharacterGuid}`
    guid: number;  // battleCharacterGuid
    unitId: number;
    unitType: number;
    name: string;
    level: number;
    groupType: BattleFieldCharacterGroupType;
    maxHp: number;
    currentHp: number;
    isDead: boolean;
    effectGroups: ActiveEffectGroup[];
}

/** 伤害/治疗事件 */
export interface DamageEvent {
    id: string;
    sourceGuid: number;
    targetGuid: number;
    targetGroupType: BattleFieldCharacterGroupType;
    value: number;  // 正值为治疗，负值为伤害
    isCrit: boolean;
    skillId: number;
    timestamp: number;
}

/** 重播事件类型 */
export type ReplayEventType = 'turn_start' | 'turn_end' | 'passive' | 'effect' | 'action' | 'damage' | 'heal';

/** 重播事件 */
export interface ReplayEvent {
    id: string;
    turn: number;
    type: ReplayEventType;
    sourceGuid?: number;
    targetGuid?: number;
    skillId?: number;
    skillDisplayType?: SkillDisplayType;
    subSkillResults?: SubSkillResult[];
    addEffectGroups?: EffectGroup[];
    removeEffectGroups?: EffectGroup[];
    changeHp?: number;
    hitType?: HitType;
    targetRemainHp?: number;
    isTurnPassive?: boolean;
    passivePhase?: 'turn_start' | 'turn_end' | 'action_start' | 'action_end' | 'action';
    timestamp: number;
    eventIndex?: number;
}

/** 状态快照 - 每个事件点预计算的完整状态 */
export interface BattleStateSnapshot {
    index: number;                          // 在快照数组中的索引
    turn: number;                           // 回合数
    eventIndex: number;                     // 回合内事件索引
    characters: Map<string, CharacterState>; // 所有角色状态
    activeDamageEvents: DamageEvent[];      // 本步产生的伤害事件（用于动画）
    event: ReplayEvent;                     // 当前事件
}

export interface UseBattleReplayReturn {
    // 状态机
    replayState: ReplayState;
    currentIndex: number;
    totalEvents: number;
    
    // 当前状态（直接从快照读取）
    currentSnapshot: BattleStateSnapshot | null;
    currentTurn: number;
    characters: Map<string, CharacterState>;
    currentEvent: ReplayEvent | null;
    activeDamageEvents: DamageEvent[];
    
    // 所有快照（用于日志展示）
    snapshots: BattleStateSnapshot[];
    
    // 播放控制
    play: () => void;
    pause: () => void;
    reset: () => void;
    goTo: (index: number) => void;
    next: () => void;
    prev: () => void;
    nextTurn: () => void;
    prevTurn: () => void;
}

/**
 * 战斗重播 Hook - 状态机版本
 * 
 * 核心思想：预计算所有状态快照，播放时直接读取
 */
export function useBattleReplay(battleData: BattleSimulationResult | null): UseBattleReplayReturn {
    const { t } = useTranslation();
    const [replayState, setReplayState] = useState<ReplayState>('idle');
    const [currentIndex, setCurrentIndex] = useState(0);
    const playIntervalRef = useRef<ReturnType<typeof setInterval> | null>(null);
    const eventIdCounter = useRef(0);

    // ===== 步骤 1: 解析原始数据为事件列表 =====
    const events = useMemo(() => {
        if (!battleData) return [];
        
        const allEvents: ReplayEvent[] = [];
        let globalEventIndex = 0;
        let timestamp = 0;
        
        const processSubSkillResult = (
            result: SubSkillResult,
            turn: number,
            phase: 'turn_start' | 'turn_end' | 'action_start' | 'action_end' | 'action',
            skillId?: number
        ) => {
            const isPassive = result.subSkillResultType === SubSkillResultType.Passive;
            const hasHpChange = result.changeHp !== 0;
            const hasAddEffects = (result.addEffectGroups?.length ?? 0) > 0;
            const isMiss = result.hitType === HitType.Miss;
            const isShieldBreak = result.hitType === HitType.ShieldBreak || result.hitType === HitType.ShieldBreakCritical;
            
            // 伤害/治疗/Miss/破盾 事件
            if (hasHpChange || isMiss || isShieldBreak) {
                const isHeal = result.skillDisplayType === SkillDisplayType.Heal || 
                               result.skillDisplayType === SkillDisplayType.SilenceHeal;
                const eventType = isHeal ? 'heal' : 'damage';
                // Miss 时 changeHp 为 0，否则根据类型计算
                const changeHpValue = isMiss ? 0 : (isHeal ? Math.abs(result.changeHp) : -Math.abs(result.changeHp));
                
                allEvents.push({
                    id: `event_${turn}_${globalEventIndex++}`,
                    turn,
                    type: eventType,
                    sourceGuid: result.attackUnitGuid,
                    targetGuid: result.targetUnitGuid,
                    skillId,
                    skillDisplayType: result.skillDisplayType,
                    changeHp: changeHpValue,
                    hitType: result.hitType,
                    targetRemainHp: result.targetRemainHp,
                    isTurnPassive: isPassive && (phase === 'turn_start' || phase === 'turn_end'),
                    passivePhase: isPassive ? phase : undefined,
                    subSkillResults: [result],
                    addEffectGroups: result.addEffectGroups,
                    removeEffectGroups: result.removeEffectGroups,
                    eventIndex: allEvents.length,
                    timestamp: timestamp++
                });
                // 破盾时不再单独生成 effect 事件（已包含在 damage 事件中）
                return;
            }
            
            // 添加效果事件
            if (hasAddEffects) {
                allEvents.push({
                    id: `effect_${turn}_${globalEventIndex++}`,
                    turn,
                    type: isPassive ? 'passive' : 'effect',
                    sourceGuid: result.attackUnitGuid,
                    targetGuid: result.targetUnitGuid,
                    skillId,
                    skillDisplayType: result.skillDisplayType,
                    isTurnPassive: isPassive && (phase === 'turn_start' || phase === 'turn_end'),
                    passivePhase: isPassive ? phase : undefined,
                    subSkillResults: [result],
                    addEffectGroups: result.addEffectGroups,
                    removeEffectGroups: result.removeEffectGroups,
                    eventIndex: allEvents.length,
                    timestamp: timestamp++
                });
            } else if (isPassive && !hasHpChange && !isMiss) {
                allEvents.push({
                    id: `passive_${turn}_${globalEventIndex++}`,
                    turn,
                    type: 'passive',
                    sourceGuid: result.attackUnitGuid,
                    targetGuid: result.targetUnitGuid,
                    skillId,
                    skillDisplayType: result.skillDisplayType,
                    isTurnPassive: phase === 'turn_start' || phase === 'turn_end',
                    passivePhase: phase,
                    subSkillResults: [result],
                    eventIndex: allEvents.length,
                    timestamp: timestamp++
                });
            }
        };
        
        battleData.battleLog?.battleSubLogs?.forEach((subLog, turnIndex) => {
            const turn = subLog.turn || turnIndex + 1;
            
            allEvents.push({
                id: `turn_start_${turn}`,
                turn,
                type: 'turn_start',
                eventIndex: allEvents.length,
                timestamp: timestamp++
            });
            
            subLog.turnStartPassiveResults?.forEach(result => {
                processSubSkillResult(result, turn, 'turn_start');
            });
            
            subLog.activeSkillDatas?.forEach((action, actionIndex) => {
                allEvents.push({
                    id: `action_start_${turn}_${actionIndex}`,
                    turn,
                    type: 'action',
                    sourceGuid: action.fromGuid,
                    skillId: action.activeSkillId,
                    eventIndex: allEvents.length,
                    timestamp: timestamp++
                });
                
                action.actionStartSubSkillResults?.forEach(result => {
                    processSubSkillResult(result, turn, 'action_start', action.activeSkillId);
                });
                
                action.subSetSkillResults?.forEach(subSet => {
                    subSet.damageSubSkillResults?.forEach(result => {
                        processSubSkillResult(result, turn, 'action', action.activeSkillId);
                    });
                    subSet.effectSubSkillResults?.forEach(result => {
                        processSubSkillResult(result, turn, 'action', action.activeSkillId);
                    });
                    subSet.passiveSubSkillResults?.forEach(result => {
                        processSubSkillResult(result, turn, 'action', action.activeSkillId);
                    });
                    subSet.tempSubSkillResults?.forEach(result => {
                        processSubSkillResult(result, turn, 'action', action.activeSkillId);
                    });
                    subSet.subSkillResults?.forEach(result => {
                        processSubSkillResult(result, turn, 'action', action.activeSkillId);
                    });
                });
                
                action.actionEndSubSkillResults?.forEach(result => {
                    processSubSkillResult(result, turn, 'action_end', action.activeSkillId);
                });
            });
            
            subLog.turnEndPassiveResults?.forEach(result => {
                processSubSkillResult(result, turn, 'turn_end');
            });
            
            allEvents.push({
                id: `turn_end_${turn}`,
                turn,
                type: 'turn_end',
                eventIndex: allEvents.length,
                timestamp: timestamp++
            });
        });
        
        return allEvents;
    }, [battleData]);
    
    // ===== 步骤 2: 预计算所有状态快照 =====
    const snapshots = useMemo(() => {
        if (!battleData || events.length === 0) return [];
        
        // 创建初始角色状态
        const createInitialCharacters = () => {
            const charMap = new Map<string, CharacterState>();
            battleData.battleCharacterReports?.forEach(report => {
                const state: CharacterState = {
                    id: `${report.groupType}-${report.battleCharacterGuid}`,
                    guid: report.battleCharacterGuid,
                    unitId: report.unitId,
                    unitType: report.unitType,
                    name: report.playerName || t('BATTLE_UNIT_CHARACTER', [report.unitId]),
                    level: report.characterLevel,
                    groupType: report.groupType,
                    maxHp: report.maxHp,
                    currentHp: report.maxHp,
                    isDead: false,
                    effectGroups: []
                };
                charMap.set(state.id, state);
            });
            return charMap;
        };
        
        // 辅助函数：查找角色
        const findCharacter = (charMap: Map<string, CharacterState>, guid: number) => {
            for (const char of charMap.values()) {
                if (char.guid === guid) return char;
            }
            return undefined;
        };
        
        // 辅助函数：克隆角色Map
        const cloneCharacters = (charMap: Map<string, CharacterState>) => {
            const cloned = new Map<string, CharacterState>();
            charMap.forEach((char, key) => {
                cloned.set(key, {
                    ...char,
                    effectGroups: char.effectGroups.map(eg => ({
                        ...eg,
                        effects: eg.effects.map(e => ({ ...e }))
                    }))
                });
            });
            return cloned;
        };
        
        // 辅助函数：应用效果组
        const applyEffectGroups = (
            charMap: Map<string, CharacterState>,
            targetGuid: number,
            effectGroups: EffectGroup[],
            isRemove: boolean
        ) => {
            const target = findCharacter(charMap, targetGuid);
            if (!target) return;
            
            if (isRemove) {
                const removeIds = effectGroups.map(eg => eg.effectGroupId);
                target.effectGroups = target.effectGroups.filter(g => !removeIds.includes(g.effectGroupId));
            } else {
                for (const eg of effectGroups) {
                    const existingIndex = target.effectGroups.findIndex(g => g.effectGroupId === eg.effectGroupId);
                    const newGroup: ActiveEffectGroup = {
                        effectGroupId: eg.effectGroupId,
                        skillCategory: eg.skillCategory,
                        effectTurn: eg.effectTurn,
                        effects: eg.effects.map(e => ({
                            effectType: e.effectType,
                            effectValue: e.effectValue,
                            effectCount: e.effectCount
                        })),
                        granterGuid: eg.granterGuid
                    };
                    if (existingIndex >= 0) {
                        target.effectGroups[existingIndex] = newGroup;
                    } else {
                        target.effectGroups.push(newGroup);
                    }
                }
            }
        };
        
        // 预计算所有快照
        const result: BattleStateSnapshot[] = [];
        const currentChars = createInitialCharacters();
        let turnEventIndex = 0;
        let currentTurn = 1;
        
        events.forEach((event, index) => {
            // 检测回合变化
            if (event.turn !== currentTurn) {
                currentTurn = event.turn;
                turnEventIndex = 0;
            }
            
            // 应用事件到当前状态
            if ((event.type === 'damage' || event.type === 'heal') && event.changeHp !== undefined) {
                const target = findCharacter(currentChars, event.targetGuid!);
                if (target) {
                    const newHp = Math.max(0, Math.min(target.maxHp, target.currentHp + event.changeHp));
                    target.currentHp = newHp;
                    target.isDead = newHp <= 0;
                }
            }
            
            // 应用效果组
            if (event.addEffectGroups?.length) {
                applyEffectGroups(currentChars, event.targetGuid!, event.addEffectGroups, false);
            }
            if (event.removeEffectGroups?.length) {
                applyEffectGroups(currentChars, event.targetGuid!, event.removeEffectGroups, true);
            }
            
            // 生成本步的伤害事件（用于动画）
            const activeDamageEvents: DamageEvent[] = [];
            if ((event.type === 'damage' || event.type === 'heal') && event.changeHp !== undefined) {
                const target = findCharacter(currentChars, event.targetGuid!);
                if (target) {
                    activeDamageEvents.push({
                        id: `dmg_${eventIdCounter.current++}`,
                        sourceGuid: event.sourceGuid ?? 0,
                        targetGuid: event.targetGuid!,
                        targetGroupType: target.groupType,
                        value: event.changeHp,
                        isCrit: event.hitType === HitType.Critical || 
                                event.hitType === HitType.Shield1Critical ||
                                event.hitType === HitType.Shield2Critical ||
                                event.hitType === HitType.ShieldBreakCritical,
                        skillId: event.skillId || 0,
                        timestamp: Date.now()
                    });
                }
            }
            
            // 创建快照（克隆当前状态）
            result.push({
                index,
                turn: event.turn,
                eventIndex: turnEventIndex,
                characters: cloneCharacters(currentChars),
                activeDamageEvents,
                event
            });
            
            turnEventIndex++;
        });
        
        return result;
    }, [battleData, events, t]);
    
    // ===== 步骤 3: 播放控制逻辑 =====
    const totalEvents = snapshots.length;
    const currentSnapshot = snapshots[currentIndex] || null;
    
    // 播放
    const play = useCallback(() => {
        if (replayState === 'playing') return;
        setReplayState('playing');
        
        playIntervalRef.current = setInterval(() => {
            setCurrentIndex(prev => {
                if (prev >= totalEvents - 1) {
                    setReplayState('ended');
                    if (playIntervalRef.current) {
                        clearInterval(playIntervalRef.current);
                        playIntervalRef.current = null;
                    }
                    return prev;
                }
                return prev + 1;
            });
        }, 1000);
    }, [replayState, totalEvents]);
    
    // 暂停
    const pause = useCallback(() => {
        setReplayState('paused');
        if (playIntervalRef.current) {
            clearInterval(playIntervalRef.current);
            playIntervalRef.current = null;
        }
    }, []);
    
    // 重置
    const reset = useCallback(() => {
        setReplayState('idle');
        setCurrentIndex(0);
        if (playIntervalRef.current) {
            clearInterval(playIntervalRef.current);
            playIntervalRef.current = null;
        }
    }, []);
    
    // 跳转到指定索引
    const goTo = useCallback((index: number) => {
        setCurrentIndex(Math.max(0, Math.min(index, totalEvents - 1)));
    }, [totalEvents]);
    
    // 下一个事件
    const next = useCallback(() => {
        setCurrentIndex(prev => Math.min(prev + 1, totalEvents - 1));
    }, [totalEvents]);
    
    // 上一个事件
    const prev = useCallback(() => {
        setCurrentIndex(prev => Math.max(prev - 1, 0));
    }, []);
    
    // 下一回合
    const nextTurn = useCallback(() => {
        const currentTurn = currentSnapshot?.turn ?? 1;
        // 找到下一个回合的第一个事件
        const nextIndex = snapshots.findIndex(s => s.turn > currentTurn);
        if (nextIndex >= 0) {
            setCurrentIndex(nextIndex);
        } else {
            setCurrentIndex(totalEvents - 1);
        }
    }, [currentSnapshot?.turn, snapshots, totalEvents]);
    
    // 上一回合
    const prevTurn = useCallback(() => {
        const currentTurn = currentSnapshot?.turn ?? 1;
        if (currentTurn <= 1) {
            setCurrentIndex(0);
            return;
        }
        // 找到当前回合的第一个事件，然后往前找一个
        const currentTurnFirstIndex = snapshots.findIndex(s => s.turn === currentTurn);
        if (currentTurnFirstIndex > 0) {
            // 找到上一回合的第一个事件
            const prevTurn = currentTurn - 1;
            const prevTurnFirstIndex = snapshots.findIndex(s => s.turn === prevTurn);
            setCurrentIndex(Math.max(0, prevTurnFirstIndex));
        }
    }, [currentSnapshot?.turn, snapshots]);
    
    // 清理定时器
    useEffect(() => {
        return () => {
            if (playIntervalRef.current) {
                clearInterval(playIntervalRef.current);
            }
        };
    }, []);
    
    // 战斗数据变化时重置
    useEffect(() => {
        setCurrentIndex(0);
        setReplayState('idle');
    }, [battleData]);
    
    return {
        // 状态机
        replayState,
        currentIndex,
        totalEvents,
        
        // 当前状态
        currentSnapshot,
        currentTurn: currentSnapshot?.turn ?? 1,
        characters: currentSnapshot?.characters ?? new Map(),
        currentEvent: currentSnapshot?.event ?? null,
        activeDamageEvents: currentSnapshot?.activeDamageEvents ?? [],
        
        // 所有快照
        snapshots,
        
        // 控制方法
        play,
        pause,
        reset,
        goTo,
        next,
        prev,
        nextTurn,
        prevTurn
    };
}
