import { useMasterStore } from '@/store/masterStore';
import { ActiveSkillMB } from '@/api/generated/activeSkillMB';
import { PassiveSkillMB } from '@/api/generated/passiveSkillMB';
import { EffectGroupMB } from '@/api/generated/effectGroupMB';
import { useLocalizationStore } from '@/store/localization-store';
import { useEffect, useState, useMemo } from 'react';
import { AssetManager } from '@/lib/asset-manager';

export interface SkillInfo {
    name: string;
    descriptionKey: string;
    iconUrl: string;
    isActiveSkill: boolean;
    isLoading: boolean;
}

/**
 * 从效果组ID转换为技能ID
 * effectId: 3900300101 -> skillId: 39003 (取前5位)
 */
function effectGroupIdToSkillId(effectGroupId: number): number {
    return Math.floor(effectGroupId / 100000);
}

/**
 * 判断是否为主动技能
 * 结尾是1或2的是主动技能
 */
function isActiveSkill(skillId: number): boolean {
    const lastDigit = skillId % 10;
    return lastDigit === 1 || lastDigit === 2;
}

/**
 * 获取单个技能信息
 */
export function useSkillInfoFromEffectGroup(effectGroupId: number): SkillInfo {
    const getRecord = useMasterStore(state => state.getRecord);
    const t = useLocalizationStore(state => state.t);
    
    const [info, setInfo] = useState<SkillInfo>({
        name: t('COMMON_SKILL_FALLBACK', [effectGroupId]),
        descriptionKey: '',
        iconUrl: '',
        isActiveSkill: false,
        isLoading: true
    });

    const skillId = useMemo(() => effectGroupIdToSkillId(effectGroupId), [effectGroupId]);
    const isActive = useMemo(() => isActiveSkill(skillId), [skillId]);

    useEffect(() => {
        let mounted = true;
        
        async function loadSkillInfo() {
            try {
                // 获取 EffectGroupMB 以获取描述 key
                const effectGroupMB = await getRecord<EffectGroupMB>('EffectGroupTable', effectGroupId);
                const descriptionKey = effectGroupMB?.descriptionKey || '';
                
                if (isActive) {
                    const activeSkillMB = await getRecord<ActiveSkillMB>('ActiveSkillTable', skillId);
                    if (activeSkillMB && mounted) {
                        setInfo({
                            name: t(activeSkillMB.nameKey) || t('COMMON_SKILL_FALLBACK', [skillId]),
                            descriptionKey,
                            iconUrl: AssetManager.skill.getCskUrl(skillId),
                            isActiveSkill: true,
                            isLoading: false
                        });
                        return;
                    }
                } else {
                    const passiveSkillMB = await getRecord<PassiveSkillMB>('PassiveSkillTable', skillId);
                    if (passiveSkillMB && mounted) {
                        setInfo({
                            name: t(passiveSkillMB.nameKey) || t('COMMON_SKILL_FALLBACK', [skillId]),
                            descriptionKey,
                            iconUrl: AssetManager.skill.getCskUrl(skillId),
                            isActiveSkill: false,
                            isLoading: false
                        });
                        return;
                    }
                }
            } catch (e) {
                console.warn('Failed to load skill info for effectGroupId:', effectGroupId, e);
            }
            
            // Fallback
            if (mounted) {
                setInfo({
                    name: t('COMMON_SKILL_FALLBACK', [skillId]),
                    descriptionKey: '',
                    iconUrl: AssetManager.skill.getCskUrl(skillId),
                    isActiveSkill: isActive,
                    isLoading: false
                });
            }
        }
        
        loadSkillInfo();
        return () => { mounted = false; };
    }, [effectGroupId, skillId, isActive, getRecord, t]);

    return info;
}

/**
 * 批量获取技能信息
 */
export function useSkillInfosFromEffectGroups(effectGroupIds: number[]): Map<number, SkillInfo> {
    const getRecord = useMasterStore(state => state.getRecord);
    const t = useLocalizationStore(state => state.t);
    const [infos, setInfos] = useState<Map<number, SkillInfo>>(new Map());

    const skillData = useMemo(() => {
        return effectGroupIds.map(id => ({
            effectGroupId: id,
            skillId: effectGroupIdToSkillId(id),
            isActive: isActiveSkill(effectGroupIdToSkillId(id))
        }));
    }, [effectGroupIds]);

    useEffect(() => {
        let mounted = true;
        
        async function loadSkills() {
            const result = new Map<number, SkillInfo>();
            
            for (const data of skillData) {
                const { effectGroupId, skillId, isActive } = data;
                let skillName = t('COMMON_SKILL_FALLBACK', [skillId]);
                let descriptionKey = '';
                
                try {
                    // 获取 EffectGroupMB 以获取描述 key
                    const effectGroupMB = await getRecord<EffectGroupMB>('EffectGroupTable', effectGroupId);
                    if (effectGroupMB) {
                        descriptionKey = effectGroupMB.descriptionKey;
                    }
                    
                    if (isActive) {
                        const activeSkillMB = await getRecord<ActiveSkillMB>('ActiveSkillTable', skillId);
                        if (activeSkillMB) {
                            skillName = t(activeSkillMB.nameKey) || skillName;
                        }
                    } else {
                        const passiveSkillMB = await getRecord<PassiveSkillMB>('PassiveSkillTable', skillId);
                        if (passiveSkillMB) {
                            skillName = t(passiveSkillMB.nameKey) || skillName;
                        }
                    }
                } catch (e) {
                    console.warn('Failed to load skill for effectGroupId:', effectGroupId, e);
                }
                
                result.set(effectGroupId, {
                    name: skillName,
                    descriptionKey,
                    iconUrl: AssetManager.skill.getCskUrl(skillId),
                    isActiveSkill: isActive,
                    isLoading: false
                });
            }
            
            if (mounted) {
                setInfos(result);
            }
        }
        
        loadSkills();
        return () => { mounted = false; };
    }, [skillData, getRecord, t]);

    return infos;
}
