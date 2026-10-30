import { useMemo } from 'react';
import { UnitType } from '@/api/generated/unitType';
import { UnitIconType } from '@/api/generated/unitIconType';
import { useMasterData } from './useMasterData';
import { useTranslation } from './useTranslation';
import { CharacterMB } from '@/api/generated/characterMB';
import { AutoBattleEnemyMB } from '@/api/generated/autoBattleEnemyMB';
import { BossBattleEnemyMB } from '@/api/generated/bossBattleEnemyMB';
import { TowerBattleEnemyMB } from '@/api/generated/towerBattleEnemyMB';
import { LocalRaidEnemyMB } from '@/api/generated/localRaidEnemyMB';
import { GuildTowerEnemyMB } from '@/api/generated/guildTowerEnemyMB';

export interface BattleUnitInfo {
    name: string;
    unitIconType: UnitIconType;
    unitIconId: number;
    isLoading: boolean;
}

/**
 * 根据 UnitType 和 UnitId 获取战斗单元信息
 */
export function useBattleUnitInfo(unitType: UnitType, unitId: number): BattleUnitInfo {
    const { t } = useTranslation();
    // 角色类型 - 查询 CharacterTable
    const { data: characterData, loading: characterLoading } = useMasterData<CharacterMB>(
        'CharacterTable',
        unitType === UnitType.Character || unitType === UnitType.ShareCharacter ? unitId : undefined
    );

    // 自动战斗敌人
    const { data: autoBattleEnemyData, loading: autoBattleLoading } = useMasterData<AutoBattleEnemyMB>(
        'AutoBattleEnemyTable',
        unitType === UnitType.AutoBattleEnemy ? unitId : undefined
    );

    // Boss 战敌人
    const { data: bossBattleEnemyData, loading: bossBattleLoading } = useMasterData<BossBattleEnemyMB>(
        'BossBattleEnemyTable',
        unitType === UnitType.BossBattleEnemy ? unitId : undefined
    );

    // 塔敌人
    const { data: towerBattleEnemyData, loading: towerBattleLoading } = useMasterData<TowerBattleEnemyMB>(
        'TowerBattleEnemyTable',
        unitType === UnitType.TowerBattleEnemy ? unitId : undefined
    );

    // 幻影神殿敌人
    const { data: localRaidEnemyData, loading: localRaidLoading } = useMasterData<LocalRaidEnemyMB>(
        'LocalRaidEnemyTable',
        unitType === UnitType.LocalRaidEnemy ? unitId : undefined
    );

    // 公会塔敌人
    const { data: guildTowerEnemyData, loading: guildTowerLoading } = useMasterData<GuildTowerEnemyMB>(
        'GuildTowerEnemyTable',
        unitType === UnitType.GuildTowerEnemy ? unitId : undefined
    );

    return useMemo(() => {
        switch (unitType) {
            case UnitType.Character:
            case UnitType.ShareCharacter:
                return {
                    name: characterData?.nameKey || t('BATTLE_UNIT_CHARACTER', [unitId]),
                    unitIconType: UnitIconType.Character,
                    unitIconId: characterData?.id ?? 0,
                    isLoading: characterLoading
                };

            case UnitType.AutoBattleEnemy:
                return {
                    name: autoBattleEnemyData?.nameKey || t('BATTLE_UNIT_ENEMY', [unitId]),
                    unitIconType: autoBattleEnemyData?.unitIconType ?? UnitIconType.EnemyCharacter,
                    unitIconId: autoBattleEnemyData?.unitIconId ?? 0,
                    isLoading: autoBattleLoading
                };

            case UnitType.BossBattleEnemy:
                return {
                    name: bossBattleEnemyData?.nameKey || `Boss${unitId}`,
                    unitIconType: bossBattleEnemyData?.unitIconType ?? UnitIconType.EnemyCharacter,
                    unitIconId: bossBattleEnemyData?.unitIconId ?? 0,
                    isLoading: bossBattleLoading
                };

            case UnitType.TowerBattleEnemy:
                return {
                    name: towerBattleEnemyData?.nameKey || t('BATTLE_UNIT_TOWER_ENEMY', [unitId]),
                    unitIconType: towerBattleEnemyData?.unitIconType ?? UnitIconType.EnemyCharacter,
                    unitIconId: towerBattleEnemyData?.unitIconId ?? 0,
                    isLoading: towerBattleLoading
                };

            case UnitType.LocalRaidEnemy:
                return {
                    name: localRaidEnemyData?.nameKey || t('BATTLE_UNIT_LOCAL_RAID_ENEMY', [unitId]),
                    unitIconType: localRaidEnemyData?.unitIconType ?? UnitIconType.EnemyCharacter,
                    unitIconId: localRaidEnemyData?.unitIconId ?? 0,
                    isLoading: localRaidLoading
                };

            case UnitType.GuildTowerEnemy:
                return {
                    name: guildTowerEnemyData?.nameKey || t('BATTLE_UNIT_GUILD_TOWER_ENEMY', [unitId]),
                    unitIconType: guildTowerEnemyData?.unitIconType ?? UnitIconType.EnemyCharacter,
                    unitIconId: guildTowerEnemyData?.unitIconId ?? 0,
                    isLoading: guildTowerLoading
                };

            case UnitType.DungeonBattleEnemy:
                return {
                    name: t('BATTLE_UNIT_DUNGEON_ENEMY', [unitId]),
                    unitIconType: UnitIconType.EnemyCharacter,
                    unitIconId: unitId,
                    isLoading: false
                };

            case UnitType.GuildRaidBoss:
                return {
                    name: t('BATTLE_UNIT_GUILD_BOSS', [unitId]),
                    unitIconType: UnitIconType.EnemyCharacter,
                    unitIconId: unitId,
                    isLoading: false
                };

            case UnitType.RentalRaidNormalEnemy:
            case UnitType.RentalRaidBossEnemy:
                return {
                    name: t('BATTLE_UNIT_RENTAL_ENEMY', [unitId]),
                    unitIconType: UnitIconType.EnemyCharacter,
                    unitIconId: unitId,
                    isLoading: false
                };

            default:
                return {
                    name: t('BATTLE_UNIT_GENERIC', [unitId]),
                    unitIconType: UnitIconType.EnemyCharacter,
                    unitIconId: unitId,
                    isLoading: false
                };
        }
    }, [
        unitType,
        unitId,
        characterData,
        characterLoading,
        autoBattleEnemyData,
        autoBattleLoading,
        bossBattleEnemyData,
        bossBattleLoading,
        towerBattleEnemyData,
        towerBattleLoading,
        localRaidEnemyData,
        localRaidLoading,
        guildTowerEnemyData,
        guildTowerLoading,
        t
    ]);
}
