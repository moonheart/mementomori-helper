import { useMasterStore } from '@/store/masterStore';
import { useLocalizationStore } from '@/store/localization-store';
import { useEffect, useState } from 'react';
import { ActiveSkillMB } from '@/api/generated/activeSkillMB';
import { PassiveSkillMB } from '@/api/generated/passiveSkillMB';

/**
 * 获取主动技能名称
 */
export function useActiveSkillName(skillId: number | undefined) {
    const t = useLocalizationStore(state => state.t);
    const getRecord = useMasterStore(state => state.getRecord);
    const [name, setName] = useState<string>('');
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        if (!skillId) {
            setName('');
            return;
        }

        setIsLoading(true);
        getRecord<ActiveSkillMB>('ActiveSkillTable', skillId)
            .then(skill => {
                setName(skill ? t(skill.nameKey) : t('COMMON_SKILL_FALLBACK', [skillId]));
            })
            .catch(() => {
                setName(t('COMMON_SKILL_FALLBACK', [skillId]));
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, [skillId, getRecord, t]);

    return { name: name || t('COMMON_SKILL_FALLBACK', [skillId]), isLoading };
}

/**
 * 获取被动技能名称
 */
export function usePassiveSkillName(skillId: number | undefined) {
    const t = useLocalizationStore(state => state.t);
    const getRecord = useMasterStore(state => state.getRecord);
    const [name, setName] = useState<string>('');
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        if (!skillId) {
            setName('');
            return;
        }

        setIsLoading(true);
        getRecord<PassiveSkillMB>('PassiveSkillTable', skillId)
            .then(skill => {
                setName(skill ? t(skill.nameKey) : t('COMMON_PASSIVE_FALLBACK', [skillId]));
            })
            .catch(() => {
                setName(t('COMMON_PASSIVE_FALLBACK', [skillId]));
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, [skillId, getRecord, t]);

    return { name: name || t('COMMON_PASSIVE_FALLBACK', [skillId]), isLoading };
}

/**
 * 获取技能名称（主动或被动）
 * @param skillId 技能ID
 * @param isPassive 是否为被动技能
 */
export function useSkillName(skillId: number | undefined, isPassive: boolean = false) {
    const activeResult = useActiveSkillName(!isPassive ? skillId : undefined);
    const passiveResult = usePassiveSkillName(isPassive ? skillId : undefined);

    return isPassive ? passiveResult : activeResult;
}
