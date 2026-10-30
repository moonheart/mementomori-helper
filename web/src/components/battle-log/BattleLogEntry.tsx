import { cn } from '@/lib/utils';
import { ReplayEvent, CharacterState } from '@/hooks/useBattleReplay';
import { SkillDisplayType } from '@/api/generated/skillDisplayType';
import { EffectType } from '@/api/generated/effectType';
import { HitType } from '@/api/generated/hitType';
import { BattleFieldCharacterGroupType } from '@/api/generated/battleFieldCharacterGroupType';
import { forwardRef } from 'react';
import { useBattleUnitInfo } from '@/hooks/useBattleUnitInfo';
import { useLocalizationStore } from '@/store/localization-store';
import { useSkillName } from '@/hooks/useSkillName';
import { useTranslation } from '@/hooks/useTranslation';

/** 效果类型翻译键映射 */
const effectTypeKeys: Record<number, string> = {
    // 增益效果
    [EffectType.SpeedUp]: 'BATTLE_LOG_EFFECT_SPEED_UP',
    [EffectType.MaxHpUp]: 'BATTLE_LOG_EFFECT_MAX_HP_UP',
    [EffectType.AttackPowerUp]: '[EffectGroupName550000000600101]',
    [EffectType.DefenseUp]: '[EffectGroupName550000000700101]',
    [EffectType.PhysicalDamageRelaxUp]: 'BATTLE_LOG_EFFECT_PHYSICAL_DAMAGE_RELAX_UP',
    [EffectType.MagicDamageRelaxUp]: 'BATTLE_LOG_EFFECT_MAGIC_DAMAGE_RELAX_UP',
    [EffectType.DamageEnhanceUp]: 'BATTLE_LOG_EFFECT_DAMAGE_ENHANCE_UP',
    [EffectType.HitUp]: 'BATTLE_LOG_EFFECT_HIT_UP',
    [EffectType.AvoidanceUp]: 'BATTLE_LOG_EFFECT_AVOIDANCE_UP',
    [EffectType.CriticalUp]: 'BATTLE_LOG_EFFECT_CRITICAL_UP',
    [EffectType.CriticalResistUp]: 'BATTLE_LOG_EFFECT_CRITICAL_RESIST_UP',
    [EffectType.HpDrainUp]: 'BATTLE_LOG_EFFECT_HP_DRAIN_UP',
    [EffectType.DamageReflectUp]: 'BATTLE_LOG_EFFECT_DAMAGE_REFLECT_UP',
    [EffectType.GiveHealRateUp]: 'BATTLE_LOG_EFFECT_GIVE_HEAL_RATE_UP',
    [EffectType.ReceiveHealRateUp]: 'BATTLE_LOG_EFFECT_RECEIVE_HEAL_RATE_UP',
    [EffectType.GiveDamageUp]: 'BATTLE_LOG_EFFECT_GIVE_DAMAGE_UP',
    [EffectType.ReceiveDamageDown]: 'BATTLE_LOG_EFFECT_RECEIVE_DAMAGE_DOWN',
    [EffectType.CoolTimeRecoveryUp]: 'BATTLE_LOG_EFFECT_COOL_TIME_RECOVERY_UP',
    [EffectType.HitRateUp]: 'BATTLE_LOG_EFFECT_HIT_RATE_UP',
    [EffectType.AvoidanceRateUp]: 'BATTLE_LOG_EFFECT_AVOIDANCE_RATE_UP',
    [EffectType.CriticalRateUp]: 'BATTLE_LOG_EFFECT_CRITICAL_RATE_UP',
    [EffectType.CriticalResistRateUp]: 'BATTLE_LOG_EFFECT_CRITICAL_RESIST_RATE_UP',
    [EffectType.DebuffHitRateUp]: 'BATTLE_LOG_EFFECT_DEBUFF_HIT_RATE_UP',
    [EffectType.DebuffResistRateUp]: 'BATTLE_LOG_EFFECT_DEBUFF_RESIST_RATE_UP',

    // 特殊增益
    [EffectType.DamageGuard]: 'BATTLE_LOG_EFFECT_DAMAGE_GUARD',
    [EffectType.Shield1]: 'BATTLE_LOG_EFFECT_SHIELD1_BUFF',
    [EffectType.Shield2]: 'BATTLE_LOG_EFFECT_SHIELD2_BUFF',
    [EffectType.DebuffGuard]: '[SkillDescriptionLinkText5]',
    [EffectType.ConfuseActionDebuffGuard]: '[SkillDescriptionLinkText6]',
    [EffectType.Taunt]: '[SkillDescriptionLinkText8]',
    [EffectType.Stealth]: 'BATTLE_LOG_EFFECT_STEALTH',
    [EffectType.NonTarget]: 'BATTLE_LOG_EFFECT_NON_TARGET',
    [EffectType.HealOverTime]: 'BATTLE_LOG_EFFECT_HEAL_OVER_TIME',
    [EffectType.Immortal]: '[SkillDescriptionLinkText13]',
    [EffectType.BuffCover]: '[SkillDescriptionLinkText16]',
    [EffectType.NonHit]: 'BATTLE_LOG_EFFECT_NON_HIT',

    // 减益效果
    [EffectType.SpeedDown]: 'BATTLE_LOG_EFFECT_SPEED_DOWN',
    [EffectType.MaxHpDown]: 'BATTLE_LOG_EFFECT_MAX_HP_DOWN',
    [EffectType.AttackPowerDown]: 'BATTLE_LOG_EFFECT_ATTACK_POWER_DOWN',
    [EffectType.DefenseDown]: 'BATTLE_LOG_EFFECT_DEFENSE_DOWN',
    [EffectType.PhysicalDamageRelaxDown]: 'BATTLE_LOG_EFFECT_PHYSICAL_DAMAGE_RELAX_DOWN',
    [EffectType.MagicDamageRelaxDown]: 'BATTLE_LOG_EFFECT_MAGIC_DAMAGE_RELAX_DOWN',
    [EffectType.DamageEnhanceDown]: 'BATTLE_LOG_EFFECT_DAMAGE_ENHANCE_DOWN',
    [EffectType.HitDown]: 'BATTLE_LOG_EFFECT_HIT_DOWN',
    [EffectType.AvoidanceDown]: 'BATTLE_LOG_EFFECT_AVOIDANCE_DOWN',
    [EffectType.CriticalDown]: 'BATTLE_LOG_EFFECT_CRITICAL_DOWN',
    [EffectType.CriticalResistDown]: 'BATTLE_LOG_EFFECT_CRITICAL_RESIST_DOWN',
    [EffectType.HpDrainDown]: 'BATTLE_LOG_EFFECT_HP_DRAIN_DOWN',
    [EffectType.DamageReflectDown]: 'BATTLE_LOG_EFFECT_DAMAGE_REFLECT_DOWN',
    [EffectType.GiveHealRateDown]: 'BATTLE_LOG_EFFECT_GIVE_HEAL_RATE_DOWN',
    [EffectType.ReceiveHealRateDown]: 'BATTLE_LOG_EFFECT_RECEIVE_HEAL_RATE_DOWN',
    [EffectType.GiveDamageDown]: 'BATTLE_LOG_EFFECT_GIVE_DAMAGE_DOWN',
    [EffectType.ReceiveDamageUp]: 'BATTLE_LOG_EFFECT_RECEIVE_DAMAGE_UP',
    [EffectType.CoolTimeRecoveryDown]: 'BATTLE_LOG_EFFECT_COOL_TIME_RECOVERY_DOWN',
    [EffectType.HitRateDown]: 'BATTLE_LOG_EFFECT_HIT_RATE_DOWN',
    [EffectType.AvoidanceRateDown]: 'BATTLE_LOG_EFFECT_AVOIDANCE_RATE_DOWN',
    [EffectType.CriticalRateDown]: 'BATTLE_LOG_EFFECT_CRITICAL_RATE_DOWN',
    [EffectType.CriticalResistRateDown]: 'BATTLE_LOG_EFFECT_CRITICAL_RESIST_RATE_DOWN',

    // 控制效果
    [EffectType.Stun]: '[SkillDescriptionLinkText7]',
    [EffectType.Confuse]: 'BATTLE_LOG_EFFECT_CONFUSE',
    [EffectType.Silence]: '[SkillDescriptionLinkText19]',
    [EffectType.Stubborn]: 'BATTLE_LOG_EFFECT_STUBBORN',

    // 持续伤害
    [EffectType.Poison]: 'BATTLE_LOG_EFFECT_POISON',
    [EffectType.Bleeding]: 'BATTLE_LOG_EFFECT_BLEEDING',
    [EffectType.Combustion]: 'BATTLE_LOG_EFFECT_COMBUSTION',
    [EffectType.Burn]: 'BATTLE_LOG_EFFECT_BURN',
    [EffectType.HpRecoveryForbidden]: 'BATTLE_LOG_EFFECT_HP_RECOVERY_FORBIDDEN',
    [EffectType.AvoidanceForbidden]: 'BATTLE_LOG_EFFECT_AVOIDANCE_FORBIDDEN',
    [EffectType.BuffForbidden]: '[SkillDescriptionLinkText29]',

    // 共鸣/献身
    [EffectType.DamageResonanceFromSelfAndDamageReduction]: 'BATTLE_LOG_EFFECT_RESONANCE_SELF',
    [EffectType.DamageResonanceFromHighHpEnemy]: 'BATTLE_LOG_EFFECT_RESONANCE_HIGH_HP',
    [EffectType.DamageResonanceFromLowHpEnemy]: 'BATTLE_LOG_EFFECT_RESONANCE_LOW_HP',
    [EffectType.DamageResonanceFromAllEnemy]: 'BATTLE_LOG_EFFECT_RESONANCE_ALL',
};

/** 技能显示类型翻译键映射 */
const skillDisplayTypeKeys: Record<SkillDisplayType, string> = {
    [SkillDisplayType.None]: '',
    [SkillDisplayType.Heal]: 'BATTLE_LOG_SKILL_HEAL',
    [SkillDisplayType.PhysicalAttack]: 'BATTLE_LOG_SKILL_PHYSICAL_ATTACK',
    [SkillDisplayType.MagicAttack]: 'BATTLE_LOG_SKILL_MAGIC_ATTACK',
    [SkillDisplayType.PhysicalDirectDamage]: 'BATTLE_LOG_SKILL_PHYSICAL_DIRECT_DAMAGE',
    [SkillDisplayType.MagicDirectDamage]: 'BATTLE_LOG_SKILL_MAGIC_DIRECT_DAMAGE',
    [SkillDisplayType.HpDrain]: '[BattleParameterTypeHpDrain]',
    [SkillDisplayType.Buff]: 'BATTLE_LOG_SKILL_BUFF',
    [SkillDisplayType.DeBuff]: 'BATTLE_LOG_SKILL_DEBUFF',
    [SkillDisplayType.PhysicalCounterAttack]: 'BATTLE_LOG_SKILL_PHYSICAL_COUNTER',
    [SkillDisplayType.MagicCounterAttack]: 'BATTLE_LOG_SKILL_MAGIC_COUNTER',
    [SkillDisplayType.PhysicalResonanceAttack]: 'BATTLE_LOG_SKILL_PHYSICAL_RESONANCE',
    [SkillDisplayType.MagicResonanceAttack]: 'BATTLE_LOG_SKILL_MAGIC_RESONANCE',
    [SkillDisplayType.RemoveEffect]: 'BATTLE_LOG_SKILL_REMOVE_EFFECT',
    [SkillDisplayType.BurstEffect]: 'BATTLE_LOG_SKILL_BURST_EFFECT',
    [SkillDisplayType.SelfInjuryDamage]: 'BATTLE_LOG_SKILL_SELF_INJURY',
    [SkillDisplayType.Resurrection]: '[DungeonBattleGridName12]',
    [SkillDisplayType.SilenceHeal]: 'BATTLE_LOG_SKILL_SILENCE_HEAL',
};

interface BattleLogEntryProps {
    event: ReplayEvent;
    characters: Map<string, CharacterState>;
    isCurrent: boolean;
    isPast: boolean;
    onClick?: () => void;
}

/**
 * 角色名字组件 - 从 Master 数据获取真实角色名，区分敌我颜色
 */
function CharacterName({ character }: { character: CharacterState | null }) {
    const t = useLocalizationStore(state => state.t);
    const { name: nameKey, isLoading } = useBattleUnitInfo(
        character?.unitType ?? 0,
        character?.unitId ?? 0
    );

    if (!character) {
        return <span>{t('[CharacterBloodTypeNone]')}</span>;
    }

    const isAttacker = character.groupType === BattleFieldCharacterGroupType.Attacker;

    if (isLoading) return <span>...</span>;

    return (
        <span className={cn(
            "font-medium",
            isAttacker
                ? "text-blue-600 dark:text-blue-400"
                : "text-red-600 dark:text-red-400"
        )}>
            {t(nameKey)}
        </span>
    );
}

/**
 * 战斗日志条目组件
 */
export const BattleLogEntry = forwardRef<HTMLDivElement, BattleLogEntryProps>(
    ({ event, characters, isCurrent, isPast, onClick }, ref) => {
        const { t } = useTranslation();

        // 通过 guid 查找角色
        const getCharacterByGuid = (guid?: number) => {
            if (guid === undefined) return null;
            for (const [, char] of characters) {
                if (char.guid === guid) return char;
            }
            return null;
        };

        const sourceChar = getCharacterByGuid(event.sourceGuid);
        const targetChar = getCharacterByGuid(event.targetGuid);

        // 获取技能名称（必须在顶层调用 hook）
        const { name: activeSkillName } = useSkillName(event.skillId, false);
        const { name: passiveSkillName } = useSkillName(event.skillId, true);

        // 获取效果文本
        const getEffectText = () => {
            if (event.addEffectGroups && event.addEffectGroups.length > 0) {
                const effects = event.addEffectGroups.flatMap(eg => eg.effects || []);
                return effects.map(e => {
                    const key = effectTypeKeys[e.effectType];
                    const label = key ? t(key) : t('BATTLE_LOG_EFFECT_FALLBACK', [String(e.effectType)]);
                    const value = e.effectValue ? ` +${e.effectValue.toLocaleString()}` : '';
                    return `${label}${value}`;
                }).join(', ');
            }
            return null;
        };

        // 获取命中类型文本
        const getHitTypeText = (hitType?: HitType) => {
            if (hitType === undefined || hitType === HitType.Hit) return null;
            switch (hitType) {
                case HitType.Ignore: return t('BATTLE_LOG_IGNORE_DEFENSE');
                case HitType.Miss: return t('[SphereCategoryTypeAvoidance]');
                case HitType.Critical: return t('[BattleParameterTypeCritical]');
                case HitType.Shield1: return t('BATTLE_LOG_SHIELD1');
                case HitType.Shield1Critical: return t('BATTLE_LOG_SHIELD1_CRITICAL');
                case HitType.Shield2: return t('BATTLE_LOG_SHIELD2');
                case HitType.Shield2Critical: return t('BATTLE_LOG_SHIELD2_CRITICAL');
                case HitType.ShieldBreak: return t('BATTLE_LOG_SHIELD_BREAK');
                case HitType.ShieldBreakCritical: return t('BATTLE_LOG_SHIELD_BREAK_CRITICAL');
                default: return null;
            }
        };

        // 渲染不同类型的事件
        const renderContent = () => {
            switch (event.type) {
                case 'turn_start':
                    return (
                        <div className="text-center text-muted-foreground font-medium py-1">
                            {t('BATTLE_LOG_TURN_START', [String(event.turn)])}
                        </div>
                    );

                case 'turn_end':
                    return (
                        <div className="text-center text-muted-foreground font-medium py-1">
                            {t('BATTLE_LOG_TURN_END', [String(event.turn)])}
                        </div>
                    );

                case 'action':
                    return (
                        <div className="flex items-center gap-2 flex-wrap">
                            <CharacterName character={sourceChar} />
                            <span className="text-muted-foreground">{t('[ItemBoxButtonUse]')}</span>
                            <span className="text-primary font-medium">{activeSkillName}</span>
                        </div>
                    );
                    
                case 'damage':
                case 'heal': {
                    const isHeal = event.type === 'heal' || (event.changeHp ?? 0) > 0;
                    const value = event.changeHp ?? 0;
                    const isMiss = event.hitType === HitType.Miss;
                    const valueText = isMiss
                        ? 'Miss'
                        : (isHeal
                            ? `+${value.toLocaleString()}`
                            : `-${Math.abs(value).toLocaleString()}`);
                    const hitText = getHitTypeText(event.hitType);
                    const effectText = getEffectText();
                    const attackTypeText = event.skillDisplayType
                        ? (skillDisplayTypeKeys[event.skillDisplayType] ? t(skillDisplayTypeKeys[event.skillDisplayType]) : null)
                        : null;

                    return (
                        <div className="flex items-center gap-1.5 flex-wrap">
                            {/* 来源 */}
                            {sourceChar && (
                                <>
                                    <CharacterName character={sourceChar} />
                                    <span className="text-muted-foreground">→</span>
                                </>
                            )}
                            {/* 目标 */}
                            <CharacterName character={targetChar} />
                            {/* 攻击类型 */}
                            {attackTypeText && (
                                <span className="text-xs text-muted-foreground">[{attackTypeText}]</span>
                            )}
                            {/* 数值/Miss */}
                            <span className={cn(
                                "font-bold ml-auto",
                                isMiss && "text-gray-500",
                                !isMiss && isHeal && "text-green-600 dark:text-green-400",
                                !isMiss && !isHeal && "text-red-600 dark:text-red-400"
                            )}>
                                {valueText}
                            </span>
                            {/* 暴击/闪避标记 */}
                            {hitText && (
                                <span className="text-yellow-500 font-medium">[{hitText}]</span>
                            )}
                            {/* 效果 */}
                            {effectText && (
                                <span className="text-xs text-muted-foreground w-full">
                                    {t('BATTLE_LOG_GAIN_EFFECT')}{effectText}
                                </span>
                            )}
                        </div>
                    );
                }

                case 'passive':
                case 'effect': {
                    const effectText = getEffectText();
                    const isBuff = event.skillDisplayType === SkillDisplayType.Buff;
                    const isDeBuff = event.skillDisplayType === SkillDisplayType.DeBuff;

                    return (
                        <div className="flex items-center gap-1.5 flex-wrap">
                            {/* 被动标记 */}
                            {event.isTurnPassive && (
                                <span className="text-xs text-orange-500 font-medium">[{t('BATTLE_LOG_PASSIVE')}]</span>
                            )}
                            {/* 来源 */}
                            {sourceChar && (
                                <>
                                    <CharacterName character={sourceChar} />
                                    {targetChar && targetChar.guid !== sourceChar.guid && (
                                        <span className="text-muted-foreground">→</span>
                                    )}
                                </>
                            )}
                            {/* 目标 */}
                            {targetChar && (!sourceChar || targetChar.guid !== sourceChar.guid) && (
                                <CharacterName character={targetChar} />
                            )}
                            {/* 技能名称 */}
                            {event.skillId && (
                                <span className="text-xs text-muted-foreground">
                                    [{event.type === 'passive' ? passiveSkillName : activeSkillName}]
                                </span>
                            )}
                            {/* 效果描述 */}
                            {effectText && (
                                <span className={cn(
                                    "text-sm",
                                    isBuff && "text-blue-600 dark:text-blue-400",
                                    isDeBuff && "text-purple-600 dark:text-purple-400"
                                )}>
                                    {effectText}
                                </span>
                            )}
                            {/* 持续回合 */}
                            {event.addEffectGroups && event.addEffectGroups.length > 0 &&
                             event.addEffectGroups[0].effectTurn > 0 && event.addEffectGroups[0].effectTurn < 9999 && (
                                <span className="text-xs text-muted-foreground">
                                    ({t('BATTLE_LOG_TURNS', [String(event.addEffectGroups[0].effectTurn)])})
                                </span>
                            )}
                        </div>
                    );
                }

                default:
                    return (
                        <div className="text-muted-foreground">
                            {t('BATTLE_LOG_UNKNOWN_EVENT', [event.type])}
                        </div>
                    );
            }
        };
        
        // 确定样式
        const isTurnMarker = event.type === 'turn_start' || event.type === 'turn_end';
        const isActionEvent = event.type === 'action';
        
        return (
            <div
                ref={ref}
                onClick={onClick}
                className={cn(
                    "px-2 py-1.5 rounded text-sm transition-all cursor-pointer",
                    // 回合标记特殊样式
                    isTurnMarker && "bg-muted/50 border-y border-border/30 my-1",
                    // 行动事件样式
                    isActionEvent && "bg-primary/5 border-l-2 border-primary/30",
                    // 当前播放事件样式
                    isCurrent && !isTurnMarker && "bg-primary/20 ring-1 ring-primary shadow-sm",
                    // 已播放事件样式
                    isPast && !isCurrent && "opacity-80",
                    // 未播放事件样式
                    !isPast && !isCurrent && "opacity-40",
                    // 鼠标悬停
                    !isTurnMarker && "hover:bg-muted/50"
                )}
            >
                {renderContent()}
            </div>
        );
    }
);

BattleLogEntry.displayName = 'BattleLogEntry';
