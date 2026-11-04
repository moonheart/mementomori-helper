import { useMemo } from 'react';
import { useMasterData } from '@/hooks/useMasterData';
import type { CharacterMB } from '@/api/generated/characterMB';
import { CharacterRarityFlags } from '@/api/generated/characterRarityFlags';
import type { UserCharacterInfo } from '@/api/generated/userCharacterInfo';
import type { UserSyncData } from '@/api/generated/userSyncData';
import { AssetManager } from '@/lib/asset-manager';
import styles from './CharacterIcon.module.css';

const isLevelLinkMember = (userSyncData: UserSyncData | null | undefined, guid: string): boolean =>
    !!userSyncData?.userLevelLinkMemberDtoInfos?.some(member => member.userCharacterGuid === guid);

const getLevelLinkLevel = (
    userSyncData: UserSyncData | null | undefined,
    characterRarity: CharacterRarityFlags
): number => {
    if (!userSyncData?.userLevelLinkDtoInfo?.partyLevel) {
        return 0;
    }

    const partyLevel = userSyncData.userLevelLinkDtoInfo.partyLevel;
    const maxLevelMap: Partial<Record<CharacterRarityFlags, number>> = {
        [CharacterRarityFlags.N]: 20,
        [CharacterRarityFlags.R]: 40,
        [CharacterRarityFlags.RPlus]: 60,
        [CharacterRarityFlags.SR]: 80,
        [CharacterRarityFlags.SRPlus]: 100,
        [CharacterRarityFlags.SSR]: 120,
        [CharacterRarityFlags.SSRPlus]: 140,
        [CharacterRarityFlags.UR]: 160,
        [CharacterRarityFlags.URPlus]: 180,
        [CharacterRarityFlags.LR]: 240,
    };

    const maxLevel = maxLevelMap[characterRarity] ?? partyLevel;
    return Math.min(maxLevel, partyLevel);
};

export interface CharacterIconProps {
    userCharacterInfo: UserCharacterInfo;
    size?: number;
    userSyncData?: UserSyncData | null;
}

export function CharacterIcon({ userCharacterInfo, size = 128, userSyncData }: CharacterIconProps) {
    const { data: characterMb } = useMasterData<CharacterMB>('CharacterTable', userCharacterInfo.characterId);
    const characterMasterFromStore = useMemo(() => characterMb ?? null, [characterMb]);

    const scale = size / 128;
    const frameUrl = AssetManager.character.getFrameUrl(userCharacterInfo.rarityFlags);
    const decorationUrl = AssetManager.character.getDecorationUrl(userCharacterInfo.rarityFlags);
    const elementUrl = AssetManager.character.getElementUrl(characterMasterFromStore?.elementType);
    const filter = AssetManager.character.getRarityFilter(userCharacterInfo.rarityFlags);

    const levelInfo = useMemo(() => {
        const levelLinkMember = isLevelLinkMember(userSyncData, userCharacterInfo.guid);
        if (!levelLinkMember) {
            return { level: userCharacterInfo.level, isLevelLink: false };
        }
        const rarity = characterMasterFromStore?.rarityFlags ?? userCharacterInfo.rarityFlags;
        const level = getLevelLinkLevel(userSyncData, rarity);
        return { level, isLevelLink: true };
    }, [userSyncData, userCharacterInfo.guid, userCharacterInfo.level, userCharacterInfo.rarityFlags, characterMasterFromStore]);

    const rarityStarCount = AssetManager.character.getRarityStarCount(userCharacterInfo.rarityFlags);
    const rarityStarIcon = AssetManager.character.getRarityStarIconUrl(characterMasterFromStore?.rarityFlags);

    return (
        <div className={styles.wrapper} style={{ width: size, height: size }}>
            <div className={styles.icon} style={{ transform: `scale(${scale})` }}>
                <div
                    className={styles.background}
                    style={{ backgroundImage: `url(${AssetManager.character.getFrameBackgroundUrl()})` }}
                />
                <div
                    className={styles.avatar}
                    style={{ backgroundImage: `url(${AssetManager.character.getAvatarUrl(userCharacterInfo.characterId)})` }}
                />
                <div
                    className={styles.border}
                    style={{ borderImageSource: `url(${frameUrl})`, filter }}
                />
                <span
                    className={styles.level}
                    style={{ color: levelInfo.isLevelLink ? '#abe4e6' : 'white' }}
                >
                    Lv{levelInfo.level}
                </span>
                {decorationUrl && (
                    <img className={styles.deco} src={decorationUrl} alt="" />
                )}
                {elementUrl && (
                    <img className={styles.element} src={elementUrl} alt="" />
                )}
                {userCharacterInfo.rarityFlags > CharacterRarityFlags.LR && rarityStarCount > 0 && (
                    <div className={styles.rarityStarContainer}>
                        {Array.from({ length: rarityStarCount }).map((_, index) => (
                            <img
                                key={`${userCharacterInfo.guid}-star-${index}`}
                                className={styles.rarityStar}
                                src={rarityStarIcon}
                                alt=""
                            />
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
}
