import { useEffect, useMemo, useState } from 'react';
import { Shield, UserPlus, Flag, MessageCircle } from 'lucide-react';
import { Dialog, DialogContent } from '@/components/ui/dialog';
import { Button } from '@/components/ui/button';
import { useTranslation } from '@/hooks/useTranslation';
import { CharacterIcon } from '@/components/character/CharacterIcon';
import { useMasterData } from '@/hooks/useMasterData';
import { CharacterDetailDialog } from '@/components/characters/CharacterDetailDialog';
import { useMasterStore } from '@/store/masterStore';
import { CharacterIconManager, AssetManager } from '@/lib/asset-manager';
import { CharacterMB, ElementType, JobFlags } from '@/api/generated';
import type { SpecialIconItemMB } from '@/api/generated/specialIconItemMB';
import { PlayerInfo } from '@/api/generated/playerInfo';
import type { UICharacter } from '@/components/characters/types';
import styles from './PlayerInfoDialog.module.css';

export interface PlayerInfoDialogProps {
    open: boolean;
    onOpenChange: (open: boolean) => void;
    player: PlayerInfo | null;
}

const formatQuestProgress = (questId?: number | null): string => {
    if (!questId || questId <= 0) return '-';
    const chapter = Math.floor(questId / 100);
    const stage = questId % 100 || 100;
    return `${chapter}-${stage}`;
};

export function PlayerInfoDialog({
    open,
    onOpenChange,
    player,
}: PlayerInfoDialogProps) {
    const { t } = useTranslation();
    const getTable = useMasterStore(state => state.getTable);
    const [characterMasterMap, setCharacterMasterMap] = useState<Record<number, CharacterMB>>({});
    const [selectedCharacter, setSelectedCharacter] = useState<UICharacter | null>(null);
    const [detailDialogOpen, setDetailDialogOpen] = useState(false);

    useEffect(() => {
        let cancelled = false;
        if (!open) return;
        getTable<CharacterMB>('CharacterTable')
            .then((table) => {
                if (cancelled) return;
                const map: Record<number, CharacterMB> = {};
                table.forEach((entry) => { map[entry.id] = entry; });
                setCharacterMasterMap(map);
            })
            .catch((error) => {
                console.error('Failed to load character master table:', error);
            });
        return () => { cancelled = true; };
    }, [getTable, open]);

    const decodedIcon = useMemo(() => {
        if (!player) return null;
        return CharacterIconManager.decodeIconId(player.mainCharacterIconId);
    }, [player]);

    const { data: specialIconItem } = useMasterData<SpecialIconItemMB>(
        'SpecialIconItemTable',
        decodedIcon?.isSpecialIcon ? decodedIcon.specialIconId : undefined
    );

    const avatarUrl = useMemo(() => {
        if (!player || !decodedIcon) return '';
        if (decodedIcon.isSpecialIcon) {
            if (!specialIconItem) return '';
            return CharacterIconManager.getSpecialIconUrl(specialIconItem.characterId);
        }
        return decodedIcon.characterId > 0 ? CharacterIconManager.getAvatarUrl(decodedIcon.characterId) : '';
    }, [player, decodedIcon, specialIconItem]);

    const portraitUrl = useMemo(() => {
        if (!player) return '';
        const characterId = player.backgroundCharacterId || decodedIcon?.characterId || 0;
        if (!characterId) return '';
        return AssetManager.character.getDetailUrl(characterId);
    }, [player, decodedIcon]);

    const handleOpenCharacterDetail = (member: PlayerInfo['deckUserCharacterInfoList'][number]) => {
        if (!player) return;
        const master = characterMasterMap[member.characterId];
        const uiCharacter: UICharacter = {
            guid: member.guid,
            playerId: player.playerId,
            characterId: member.characterId,
            level: member.level,
            exp: member.exp,
            rarityFlags: member.rarityFlags,
            isLocked: false,
            master,
            nameKey: master?.nameKey ?? `Character ${member.characterId}`,
            element: master?.elementType ?? ElementType.None,
            job: master?.jobFlags ?? JobFlags.None,
        };
        setSelectedCharacter(uiCharacter);
        setDetailDialogOpen(true);
    };

    return (
        <Dialog open={open} onOpenChange={onOpenChange}>
            <DialogContent className={styles.dialog}>
                {!player ? (
                    <div className={styles.empty}>{t('[NoData]')}</div>
                ) : (
                    <div className={styles.body}>
                        <div className={styles.leftPanel}>
                            {portraitUrl ? (
                                <img className={styles.portrait} src={portraitUrl} alt={player.playerName} />
                            ) : (
                                <div className={styles.portraitFallback} />
                            )}
                        </div>
                        <div className={styles.rightPanel}>
                            <div className={styles.basicInfo}>
                                <div className={styles.nameRow}>
                                    <span className={styles.playerName}>{player.playerName || player.npcNameKey || '-'}</span>
                                    <span className={styles.playerId}>(ID: {player.playerId})</span>
                                </div>
                                <div className={styles.infoRowGroup}>
                                    <div className={styles.avatarBlock}>
                                        {avatarUrl ? (
                                            <img className={styles.avatar} src={avatarUrl} alt={player.playerName} />
                                        ) : (
                                            <div className={styles.avatarFallback} />
                                        )}
                                    </div>
                                    <div className={styles.infoGrid}>
                                        <div className={styles.infoRow}>
                                            <span className={styles.infoLabel}>{t('[CommonPlayerRankLabel]')}</span>
                                            <span className={styles.infoValue}>{player.playerLevel}</span>
                                        </div>
                                        <div className={styles.infoRow}>
                                            <span className={styles.infoLabel}>{t('[RankingStageLabel]')}</span>
                                            <span className={styles.infoValue}>{formatQuestProgress(player.latestQuestId)}</span>
                                        </div>
                                        <div className={styles.infoRow}>
                                            <span className={styles.infoLabel}>{t('[CommonBattlePowerLabel]')}</span>
                                            <span className={styles.infoValue}>{player.battlePower.toLocaleString()}</span>
                                        </div>
                                        <div className={styles.infoRow}>
                                            <span className={styles.infoLabel}>{t('[MyPagePlayerInformationBelongingGuildLabel]')}</span>
                                            <span className={styles.infoValue}>{player.guildName || '-'}</span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className={styles.teamSection}>
                                <div className={styles.sectionTitle}>{t('[DialogPlayerInformationAutoBattleFormationLabel]')}</div>
                                <div className={styles.teamRow}>
                                    {player.deckUserCharacterInfoList?.slice(0, 5).map((member) => (
                                        <button
                                            key={member.guid}
                                            type="button"
                                            className={styles.teamCharacter}
                                            onClick={() => handleOpenCharacterDetail(member)}
                                        >
                                            <CharacterIcon userCharacterInfo={member} size={84} />
                                        </button>
                                    ))}
                                    {!player.deckUserCharacterInfoList?.length && (
                                        <div className={styles.emptyTeam}>{t('[NoData]')}</div>
                                    )}
                                </div>
                            </div>

                            <div className={styles.commentSection}>
                                <div className={styles.commentBox}>{player.comment || t('[NoData]')}</div>
                            </div>

                            <div className={styles.actions}>
                                <Button
                                    variant="secondary"
                                    className={styles.actionButton}
                                    onClick={() => {
                                        // TODO: 接入屏蔽玩家接口
                                    }}
                                >
                                    <Shield className={styles.actionIcon} />
                                    {t('[FriendTabBlock]')}
                                </Button>
                                <Button
                                    variant="secondary"
                                    className={styles.actionButton}
                                    onClick={() => {
                                        // TODO: 接入好友申请接口
                                    }}
                                >
                                    <UserPlus className={styles.actionIcon} />
                                    {t('[FriendTabApplying]')}
                                </Button>
                                <Button
                                    variant="secondary"
                                    className={styles.actionButton}
                                    onClick={() => {
                                        // TODO: 接入举报接口
                                    }}
                                >
                                    <Flag className={styles.actionIcon} />
                                    {t('[PlayerInformationReportLabel]')}
                                </Button>
                                <Button
                                    variant="secondary"
                                    className={styles.actionButton}
                                    onClick={() => {
                                        // TODO: 接入私信接口
                                    }}
                                >
                                    <MessageCircle className={styles.actionIcon} />
                                    {t('[PlayerInformationPrivateChatLabel]')}
                                </Button>
                            </div>
                        </div>
                    </div>
                )}
            </DialogContent>
            <CharacterDetailDialog
                character={selectedCharacter}
                open={detailDialogOpen}
                onOpenChange={setDetailDialogOpen}
            />
        </Dialog>
    );
}
