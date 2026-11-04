import type {
    BaseParameter,
    BaseParameterChangeInfo,
    BaseParameterType,
    BattleParameter,
    BattleParameterChangeInfo,
    BattleParameterType,
    CharacterCollectionLevelMB,
    CharacterCollectionMB,
    CharacterCollectionParameterInfo,
    CharacterMB,
    CharacterPotentialCoefficientMB,
    CharacterPotentialMB,
    CharacterRarityFlags,
    EquipmentExclusiveEffectMB,
    EquipmentLegendSacredTreasureMB,
    EquipmentMatchlessSacredTreasureMB,
    EquipmentMB,
    EquipmentReinforcementParameterMB,
    EquipmentSetMB,
    LockEquipmentDeckType,
    PlayerRankMB,
    SphereMB,
    UserCharacterCollectionDtoInfo,
    UserCharacterDtoInfo,
    UserCharacterInfo,
    UserEquipmentDtoInfo,
    UserSyncData,
} from '@/api/generated';
import { BattleParameterType as BattleParameterTypeEnum } from '@/api/generated/battleParameterType';
import { BaseParameterType as BaseParameterTypeEnum } from '@/api/generated/baseParameterType';
import { ChangeParameterType as ChangeParameterTypeEnum } from '@/api/generated/changeParameterType';
import { EquipmentSlotType } from '@/api/generated/equipmentSlotType';
import { CharacterRarityFlags as CharacterRarityFlagsEnum } from '@/api/generated/characterRarityFlags';
import { JobFlags } from '@/api/generated/jobFlags';

export interface CharacterDetailCalculationContext {
    userSyncData: UserSyncData;
    userCharacterInfo: UserCharacterInfo;
    lockEquipmentDeckType?: LockEquipmentDeckType;
    masters: {
        characterTable: CharacterMB[];
        equipmentTable: EquipmentMB[];
        sphereTable: SphereMB[];
        equipmentSetTable: EquipmentSetMB[];
        equipmentExclusiveEffectTable: EquipmentExclusiveEffectMB[];
        equipmentReinforcementParameterTable: EquipmentReinforcementParameterMB[];
        equipmentLegendSacredTreasureTable: EquipmentLegendSacredTreasureMB[];
        equipmentMatchlessSacredTreasureTable: EquipmentMatchlessSacredTreasureMB[];
        characterCollectionTable: CharacterCollectionMB[];
        characterCollectionLevelTable: CharacterCollectionLevelMB[];
        characterPotentialTable: CharacterPotentialMB[];
        characterPotentialCoefficientTable: CharacterPotentialCoefficientMB[];
        playerRankTable: PlayerRankMB[];
    };
}

export interface CharacterDetailCalculationResult {
    baseParameter: BaseParameter;
    battleParameter: BattleParameter;
    battlePower: number;
    userEquipmentDtoInfos: UserEquipmentDtoInfo[];
}

interface MasterTableMaps {
    characterById: Record<number, CharacterMB>;
    equipmentById: Record<number, EquipmentMB>;
    sphereById: Record<number, SphereMB>;
    equipmentSetById: Record<number, EquipmentSetMB>;
    equipmentExclusiveEffectByIdAndCharacter: Record<string, EquipmentExclusiveEffectMB>;
    equipmentReinforcementByLevel: Record<number, EquipmentReinforcementParameterMB>;
    equipmentLegendSacredTreasureByLevel: Record<number, EquipmentLegendSacredTreasureMB>;
    equipmentMatchlessSacredTreasureByLevel: Record<number, EquipmentMatchlessSacredTreasureMB>;
    characterCollectionById: Record<number, CharacterCollectionMB>;
    characterCollectionLevelByCollectionLevel: Record<string, CharacterCollectionLevelMB>;
    characterPotentialByLevelAndSubLevel: Record<string, CharacterPotentialMB>;
    characterPotentialCoefficientByInitialAndNow: Record<string, CharacterPotentialCoefficientMB>;
    playerRankByRank: Record<number, PlayerRankMB>;
}

const RARITY_LEVEL_CAP_MAP: Partial<Record<CharacterRarityFlags, number>> = {
    [CharacterRarityFlagsEnum.N]: 20,
    [CharacterRarityFlagsEnum.R]: 40,
    [CharacterRarityFlagsEnum.RPlus]: 60,
    [CharacterRarityFlagsEnum.SR]: 80,
    [CharacterRarityFlagsEnum.SRPlus]: 100,
    [CharacterRarityFlagsEnum.SSR]: 120,
    [CharacterRarityFlagsEnum.SSRPlus]: 140,
    [CharacterRarityFlagsEnum.UR]: 160,
    [CharacterRarityFlagsEnum.URPlus]: 180,
    [CharacterRarityFlagsEnum.LR]: 240,
};

const BATTLE_PARAMETER_TYPES: BattleParameterType[] = [
    BattleParameterTypeEnum.Hp,
    BattleParameterTypeEnum.AttackPower,
    BattleParameterTypeEnum.PhysicalDamageRelax,
    BattleParameterTypeEnum.MagicDamageRelax,
    BattleParameterTypeEnum.Hit,
    BattleParameterTypeEnum.Avoidance,
    BattleParameterTypeEnum.Critical,
    BattleParameterTypeEnum.CriticalResist,
    BattleParameterTypeEnum.CriticalDamageEnhance,
    BattleParameterTypeEnum.PhysicalCriticalDamageRelax,
    BattleParameterTypeEnum.MagicCriticalDamageRelax,
    BattleParameterTypeEnum.DefensePenetration,
    BattleParameterTypeEnum.Defense,
    BattleParameterTypeEnum.DamageEnhance,
    BattleParameterTypeEnum.DebuffHit,
    BattleParameterTypeEnum.DebuffResist,
    BattleParameterTypeEnum.DamageReflect,
    BattleParameterTypeEnum.HpDrain,
    BattleParameterTypeEnum.Speed,
];

export function calculateCharacterDetailInfo(
    context: CharacterDetailCalculationContext
): CharacterDetailCalculationResult {
    const lockType = context.lockEquipmentDeckType ?? 0;
    const maps = buildMasterMaps(context.masters);

    const userEquipmentDtoInfos = getUserEquipmentDtoInfosByCharacterGuid(
        context.userSyncData,
        context.userCharacterInfo.guid,
        lockType
    );

    const characterCollectionParameterInfo = calcCharacterCollectionParameterInfo(
        context.userSyncData.userCharacterCollectionDtoInfos ?? [],
        maps
    );

    const rank = context.userSyncData.userStatusDtoInfo?.rank ?? 0;
    const { baseParameter, battleParameter } = calcCharacterBattleParameter(
        context.userCharacterInfo,
        userEquipmentDtoInfos,
        characterCollectionParameterInfo,
        rank,
        maps
    );

    const battlePower = calculateBattlePower(battleParameter, baseParameter);

    return {
        baseParameter,
        battleParameter,
        battlePower,
        userEquipmentDtoInfos,
    };
}

export function getUserCharacterInfoByGuid(
    userSyncData: UserSyncData,
    userCharacterGuid: string,
    characterTable: CharacterMB[]
): UserCharacterInfo | null {
    const dto = userSyncData.userCharacterDtoInfos?.find((item) => item.guid === userCharacterGuid);
    if (!dto) return null;
    return getUserCharacterInfoByDto(userSyncData, dto, characterTable);
}

export function getUserCharacterInfoByDto(
    userSyncData: UserSyncData,
    userCharacterDtoInfo: UserCharacterDtoInfo,
    characterTable: CharacterMB[]
): UserCharacterInfo {
    const rarity = characterTable.find((item) => item.id === userCharacterDtoInfo.characterId)?.rarityFlags
        ?? userCharacterDtoInfo.rarityFlags;
    const levelLinkMember = isLevelLinkMember(userSyncData, userCharacterDtoInfo.guid);
    let level = userCharacterDtoInfo.level;
    let subLevel = 0;
    if (levelLinkMember) {
        level = getLevelLinkLevel(userSyncData, rarity);
        subLevel = userSyncData.userLevelLinkDtoInfo?.partySubLevel ?? 0;
    }

    return {
        guid: userCharacterDtoInfo.guid,
        playerId: userCharacterDtoInfo.playerId,
        characterId: userCharacterDtoInfo.characterId,
        level,
        subLevel,
        exp: userCharacterDtoInfo.exp,
        rarityFlags: userCharacterDtoInfo.rarityFlags,
        isLocked: userCharacterDtoInfo.isLocked,
    } as UserCharacterInfo;
}

function getLevelLinkLevel(userSyncData: UserSyncData, characterRarity: CharacterRarityFlags): number {
    const partyLevel = userSyncData.userLevelLinkDtoInfo?.partyLevel ?? 0;
    if (!partyLevel) return 0;
    const maxLevel = RARITY_LEVEL_CAP_MAP[characterRarity] ?? partyLevel;
    return Math.min(maxLevel, partyLevel);
}

function isLevelLinkMember(userSyncData: UserSyncData, guid: string): boolean {
    return !!userSyncData.userLevelLinkMemberDtoInfos?.some((member) => member.userCharacterGuid === guid);
}

function getUserEquipmentDtoInfosByCharacterGuid(
    userSyncData: UserSyncData,
    characterGuid: string,
    lockEquipmentDeckType: LockEquipmentDeckType
): UserEquipmentDtoInfo[] {
    if (!characterGuid) return [];
    const lockedMap = userSyncData.lockedEquipmentCharacterGuidListMap ?? {};
    const lockedGuids = lockedMap[lockEquipmentDeckType] ?? [];
    if (lockedGuids.length > 0) {
        return getLockedUserEquipmentDtoInfosByCharacterGuid(userSyncData, characterGuid, lockEquipmentDeckType);
    }

    return (userSyncData.userEquipmentDtoInfos ?? []).filter(
        (equipment) => equipment.characterGuid === characterGuid
    );
}

function getLockedUserEquipmentDtoInfosByCharacterGuid(
    userSyncData: UserSyncData,
    characterGuid: string,
    lockEquipmentDeckType: LockEquipmentDeckType
): UserEquipmentDtoInfo[] {
    const lockedListMap = userSyncData.lockedUserEquipmentDtoInfoListMap ?? {};
    const lockedList = lockedListMap[lockEquipmentDeckType] ?? [];
    return lockedList.filter((equipment) => equipment.characterGuid === characterGuid);
}

function calcCharacterCollectionParameterInfo(
    userCharacterCollectionDtoInfos: UserCharacterCollectionDtoInfo[],
    maps: MasterTableMaps
): CharacterCollectionParameterInfo {
    const info: CharacterCollectionParameterInfo = {
        allCharacterBaseParameterChangeInfos: [],
        allCharacterBattleParameterChangeInfos: [],
        eachCharacterBaseParameterChangeInfoDict: {},
        eachCharacterBattleParameterChangeInfoDict: {},
    } as CharacterCollectionParameterInfo;

    userCharacterCollectionDtoInfos.forEach((collectionDtoInfo) => {
        const collection = maps.characterCollectionById[collectionDtoInfo.characterCollectionId];
        const levelKey = `${collectionDtoInfo.characterCollectionId}_${collectionDtoInfo.collectionLevel}`;
        const level = maps.characterCollectionLevelByCollectionLevel[levelKey];
        if (!collection || !level) return;

        const baseInfos = level.baseParameterChangeInfos ?? [];
        const battleInfos = level.battleParameterChangeInfos ?? [];
        if (collectionDtoInfo.collectionLevel >= 3) {
            info.allCharacterBaseParameterChangeInfos.push(...baseInfos);
            info.allCharacterBattleParameterChangeInfos.push(...battleInfos);
            return;
        }

        (collection.requiredCharacterIds ?? []).forEach((requiredId) => {
            if (baseInfos.length > 0) {
                const existingBase = info.eachCharacterBaseParameterChangeInfoDict[requiredId] ?? [];
                existingBase.push(...baseInfos);
                info.eachCharacterBaseParameterChangeInfoDict[requiredId] = existingBase;
            }
            if (battleInfos.length > 0) {
                const existingBattle = info.eachCharacterBattleParameterChangeInfoDict[requiredId] ?? [];
                existingBattle.push(...battleInfos);
                info.eachCharacterBattleParameterChangeInfoDict[requiredId] = existingBattle;
            }
        });
    });

    return info;
}

function calcCharacterBattleParameter(
    userCharacterInfo: UserCharacterInfo,
    userEquipmentDtoInfos: UserEquipmentDtoInfo[],
    characterCollectionParameterInfo: CharacterCollectionParameterInfo,
    playerRank: number,
    maps: MasterTableMaps
): { baseParameter: BaseParameter; battleParameter: BattleParameter } {
    const characterMb = maps.characterById[userCharacterInfo.characterId];
    if (!characterMb) {
        return {
            baseParameter: createEmptyBaseParameter(),
            battleParameter: createEmptyBattleParameter(),
        };
    }

    const mainParameterType = getMainBaseParameterType(characterMb.jobFlags);
    const playerRankMb = maps.playerRankByRank[playerRank] ?? ({} as PlayerRankMB);

    let baseParameter = calcBaseParameter(
        characterMb,
        userCharacterInfo.level,
        userCharacterInfo.subLevel,
        characterMb.rarityFlags,
        userCharacterInfo.rarityFlags,
        maps
    );

    baseParameter = addBaseParameter(baseParameter, getEquipmentBaseParameter(userEquipmentDtoInfos));

    const baseParameterChangeInfos: BaseParameterChangeInfo[] = [];
    const battleParameterChangeInfos: BattleParameterChangeInfo[] = [];

    const sphereParameters = getParameterOfSphere(userEquipmentDtoInfos, maps);
    baseParameterChangeInfos.push(...sphereParameters.base);
    battleParameterChangeInfos.push(...sphereParameters.battle);

    battleParameterChangeInfos.push(...getBattleParameterListOfEquipment(userEquipmentDtoInfos, maps));
    battleParameterChangeInfos.push(...getMatchlessSacredTreasureParameterList(userEquipmentDtoInfos, maps));
    battleParameterChangeInfos.push(...getLegendSacredTreasureParameterList(userEquipmentDtoInfos, maps));

    const setEquipmentEffect = getSetEquipmentEffect(userCharacterInfo.characterId, userEquipmentDtoInfos, maps);
    baseParameterChangeInfos.push(...setEquipmentEffect.base);
    battleParameterChangeInfos.push(...setEquipmentEffect.battle);

    baseParameterChangeInfos.push(...(characterCollectionParameterInfo.allCharacterBaseParameterChangeInfos ?? []));
    battleParameterChangeInfos.push(...(characterCollectionParameterInfo.allCharacterBattleParameterChangeInfos ?? []));

    const eachBase = characterCollectionParameterInfo.eachCharacterBaseParameterChangeInfoDict?.[userCharacterInfo.characterId];
    if (eachBase?.length) {
        baseParameterChangeInfos.push(...eachBase);
    }
    const eachBattle = characterCollectionParameterInfo.eachCharacterBattleParameterChangeInfoDict?.[userCharacterInfo.characterId];
    if (eachBattle?.length) {
        battleParameterChangeInfos.push(...eachBattle);
    }

    applyBaseParameterChanges(baseParameter, baseParameterChangeInfos, userCharacterInfo.level);
    const battleParameterDict = getBattleParameterDict(battleParameterChangeInfos, userCharacterInfo.level);

    const battleParameter = createBattleParameter(
        cloneBattleParameter(characterMb.initialBattleParameter),
        baseParameter,
        mainParameterType,
        playerRankMb,
        battleParameterDict.addParameters,
        battleParameterDict.mulParameters
    );

    return { baseParameter, battleParameter };
}

function calcBaseParameter(
    characterMb: CharacterMB,
    level: number,
    subLevel: number,
    initialRarityFlags: CharacterRarityFlags,
    nowRarityFlags: CharacterRarityFlags,
    maps: MasterTableMaps
): BaseParameter {
    const key = `${level}_${subLevel}`;
    const characterPotentialMb = maps.characterPotentialByLevelAndSubLevel[key];
    const coeffKey = `${initialRarityFlags}_${nowRarityFlags}`;
    const characterPotentialCoefficientMb = maps.characterPotentialCoefficientByInitialAndNow[coeffKey];

    const totalBaseParameter = characterPotentialMb?.totalBaseParameter ?? 0;
    const rarityCoefficient = characterPotentialCoefficientMb?.rarityCoefficientInfo ?? { m: 0, b: 0 };
    const v25 = totalBaseParameter * rarityCoefficient.m + rarityCoefficient.b;

    let baseParameterGrossCoefficient = characterMb.baseParameterGrossCoefficient ?? 0;
    if (!baseParameterGrossCoefficient) {
        baseParameterGrossCoefficient = getBaseParameterTotal(characterMb.baseParameterCoefficient);
        if (!baseParameterGrossCoefficient) {
            baseParameterGrossCoefficient = 1;
        }
    }

    return {
        energy: Math.trunc((characterMb.baseParameterCoefficient?.energy ?? 0) * v25 / baseParameterGrossCoefficient),
        health: Math.trunc((characterMb.baseParameterCoefficient?.health ?? 0) * v25 / baseParameterGrossCoefficient),
        intelligence: Math.trunc((characterMb.baseParameterCoefficient?.intelligence ?? 0) * v25 / baseParameterGrossCoefficient),
        muscle: Math.trunc((characterMb.baseParameterCoefficient?.muscle ?? 0) * v25 / baseParameterGrossCoefficient),
    } as BaseParameter;
}

function getBaseParameterTotal(baseParameter: BaseParameter | undefined): number {
    if (!baseParameter) return 0;
    return (baseParameter.energy ?? 0)
        + (baseParameter.health ?? 0)
        + (baseParameter.intelligence ?? 0)
        + (baseParameter.muscle ?? 0);
}

function getMainBaseParameterType(jobFlags: number): BaseParameterType {
    if (jobFlags & JobFlags.Warrior) return BaseParameterTypeEnum.Muscle;
    if (jobFlags & JobFlags.Sniper) return BaseParameterTypeEnum.Energy;
    if (jobFlags & JobFlags.Sorcerer) return BaseParameterTypeEnum.Intelligence;
    return BaseParameterTypeEnum.Health;
}

function addBaseParameter(base: BaseParameter, add: BaseParameter): BaseParameter {
    return {
        energy: (base.energy ?? 0) + (add.energy ?? 0),
        health: (base.health ?? 0) + (add.health ?? 0),
        intelligence: (base.intelligence ?? 0) + (add.intelligence ?? 0),
        muscle: (base.muscle ?? 0) + (add.muscle ?? 0),
    } as BaseParameter;
}

function getEquipmentBaseParameter(equipmentInfoList: UserEquipmentDtoInfo[]): BaseParameter {
    const base: BaseParameter = { energy: 0, health: 0, intelligence: 0, muscle: 0 } as BaseParameter;
    equipmentInfoList.forEach((equipment) => {
        base.muscle += equipment.additionalParameterMuscle ?? 0;
        base.health += equipment.additionalParameterHealth ?? 0;
        base.energy += equipment.additionalParameterEnergy ?? 0;
        base.intelligence += equipment.additionalParameterIntelligence ?? 0;
    });
    return base;
}

function getParameterOfSphere(
    equipmentList: UserEquipmentDtoInfo[],
    maps: MasterTableMaps
): { base: BaseParameterChangeInfo[]; battle: BattleParameterChangeInfo[] } {
    const base: BaseParameterChangeInfo[] = [];
    const battle: BattleParameterChangeInfo[] = [];
    equipmentList.forEach((equipment) => {
        [equipment.sphereId1, equipment.sphereId2, equipment.sphereId3, equipment.sphereId4].forEach((sphereId) => {
            if (!sphereId) return;
            const sphere = maps.sphereById[sphereId];
            if (sphere?.baseParameterChangeInfo) base.push(sphere.baseParameterChangeInfo);
            if (sphere?.battleParameterChangeInfo) battle.push(sphere.battleParameterChangeInfo);
        });
    });
    return { base, battle };
}

function getBattleParameterListOfEquipment(
    equipmentInfoList: UserEquipmentDtoInfo[],
    maps: MasterTableMaps
): BattleParameterChangeInfo[] {
    return equipmentInfoList.map((equipment) => {
        const equipmentMb = maps.equipmentById[equipment.equipmentId];
        const reinforcement = maps.equipmentReinforcementByLevel[equipment.reinforcementLv ?? 0];
        const ratio = reinforcement?.reinforcementCoefficient ?? 1.0;
        if (!equipmentMb?.battleParameterChangeInfo) {
            return {
                battleParameterType: BattleParameterTypeEnum.Hp,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: 0,
            } as BattleParameterChangeInfo;
        }
        return {
            battleParameterType: equipmentMb.battleParameterChangeInfo.battleParameterType,
            changeParameterType: equipmentMb.battleParameterChangeInfo.changeParameterType,
            value: (equipmentMb.battleParameterChangeInfo.value ?? 0) * ratio,
        } as BattleParameterChangeInfo;
    }).filter((info) => info.value !== 0);
}

function getLegendSacredTreasureParameterList(
    equipmentInfoList: UserEquipmentDtoInfo[],
    maps: MasterTableMaps
): BattleParameterChangeInfo[] {
    const list: BattleParameterChangeInfo[] = [];
    equipmentInfoList.forEach((equipment) => {
        if ((equipment.legendSacredTreasureLv ?? 0) > 0) {
            const equipmentMb = maps.equipmentById[equipment.equipmentId];
            const legendMb = maps.equipmentLegendSacredTreasureByLevel[equipment.legendSacredTreasureLv ?? 0];
            if (!equipmentMb || !legendMb) return;
            list.push(getLegendSacredTreasureValue(legendMb, equipmentMb.slotType));
        }
    });
    return list;
}

function getMatchlessSacredTreasureParameterList(
    equipmentInfoList: UserEquipmentDtoInfo[],
    maps: MasterTableMaps
): BattleParameterChangeInfo[] {
    const list: BattleParameterChangeInfo[] = [];
    equipmentInfoList.forEach((equipment) => {
        if ((equipment.matchlessSacredTreasureLv ?? 0) > 0) {
            const equipmentMb = maps.equipmentById[equipment.equipmentId];
            const matchlessMb = maps.equipmentMatchlessSacredTreasureByLevel[equipment.matchlessSacredTreasureLv ?? 0];
            if (!equipmentMb || !matchlessMb) return;
            list.push(getMatchlessSacredTreasureValue(matchlessMb, equipmentMb.slotType));
        }
    });
    return list;
}

function getLegendSacredTreasureValue(
    legendMb: EquipmentLegendSacredTreasureMB,
    slotType: EquipmentSlotType
): BattleParameterChangeInfo {
    switch (slotType) {
        case EquipmentSlotType.Weapon:
            return {
                battleParameterType: BattleParameterTypeEnum.AttackPower,
                changeParameterType: ChangeParameterTypeEnum.AdditionPercent,
                value: legendMb.weaponAttackPowerPercent ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Sub:
            return {
                battleParameterType: BattleParameterTypeEnum.Hit,
                changeParameterType: ChangeParameterTypeEnum.AdditionPercent,
                value: legendMb.subHitPercent ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Gauntlet:
            return {
                battleParameterType: BattleParameterTypeEnum.CriticalDamageEnhance,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: legendMb.gauntletCriticalDamagePercent ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Helmet:
            return {
                battleParameterType: BattleParameterTypeEnum.PhysicalCriticalDamageRelax,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: legendMb.helmetPhysicalCriticalDamageRelaxPercent ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Armor:
            return {
                battleParameterType: BattleParameterTypeEnum.MagicCriticalDamageRelax,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: legendMb.armorMagicCriticalDamageRelaxPercent ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Shoes:
            return {
                battleParameterType: BattleParameterTypeEnum.HpDrain,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: legendMb.shoesHpDrainPercent ?? 0,
            } as BattleParameterChangeInfo;
        default:
            return {
                battleParameterType: BattleParameterTypeEnum.Hp,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: 0,
            } as BattleParameterChangeInfo;
    }
}

function getMatchlessSacredTreasureValue(
    matchlessMb: EquipmentMatchlessSacredTreasureMB,
    slotType: EquipmentSlotType
): BattleParameterChangeInfo {
    switch (slotType) {
        case EquipmentSlotType.Weapon:
            return {
                battleParameterType: BattleParameterTypeEnum.AttackPower,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: matchlessMb.weaponAttackPower ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Sub:
            return {
                battleParameterType: BattleParameterTypeEnum.PhysicalDamageRelax,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: matchlessMb.subPhysicalDamageRelax ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Gauntlet:
            return {
                battleParameterType: BattleParameterTypeEnum.MagicDamageRelax,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: matchlessMb.gauntletMagicDamageRelax ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Helmet:
            return {
                battleParameterType: BattleParameterTypeEnum.Critical,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: matchlessMb.helmetCritical ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Armor:
            return {
                battleParameterType: BattleParameterTypeEnum.DefensePenetration,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: matchlessMb.armorDefensePenetration ?? 0,
            } as BattleParameterChangeInfo;
        case EquipmentSlotType.Shoes:
            return {
                battleParameterType: BattleParameterTypeEnum.Hp,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: matchlessMb.shoesHp ?? 0,
            } as BattleParameterChangeInfo;
        default:
            return {
                battleParameterType: BattleParameterTypeEnum.Hp,
                changeParameterType: ChangeParameterTypeEnum.Addition,
                value: 0,
            } as BattleParameterChangeInfo;
    }
}

function getSetEquipmentEffect(
    characterId: number,
    userEquipmentDtoInfos: UserEquipmentDtoInfo[],
    maps: MasterTableMaps
): { base: BaseParameterChangeInfo[]; battle: BattleParameterChangeInfo[] } {
    const base: BaseParameterChangeInfo[] = [];
    const battle: BattleParameterChangeInfo[] = [];
    const equipmentSetCount: Record<number, number> = {};

    userEquipmentDtoInfos.forEach((equipment) => {
        const equipmentMb = maps.equipmentById[equipment.equipmentId];
        if (!equipmentMb) return;

        if (equipmentMb.equipmentSetId > 0) {
            equipmentSetCount[equipmentMb.equipmentSetId] = (equipmentSetCount[equipmentMb.equipmentSetId] ?? 0) + 1;
        }

        if (equipmentMb.exclusiveEffectId > 0) {
            const key = `${equipmentMb.exclusiveEffectId}_${characterId}`;
            const exclusive = maps.equipmentExclusiveEffectByIdAndCharacter[key];
            if (exclusive?.baseParameterChangeInfoList?.length) {
                base.push(...exclusive.baseParameterChangeInfoList);
            }
            if (exclusive?.battleParameterChangeInfoList?.length) {
                battle.push(...exclusive.battleParameterChangeInfoList);
            }
        }
    });

    Object.entries(equipmentSetCount).forEach(([setIdStr, count]) => {
        const setId = Number(setIdStr);
        const setMb = maps.equipmentSetById[setId];
        if (!setMb) return;
        (setMb.effectList ?? []).forEach((effect) => {
            if ((count ?? 0) >= (effect.requiredEquipmentCount ?? 0)) {
                if (effect.baseParameterChangeInfo) base.push(effect.baseParameterChangeInfo);
                if (effect.battleParameterChangeInfo) battle.push(effect.battleParameterChangeInfo);
            }
        });
    });

    return { base, battle };
}

function applyBaseParameterChanges(
    baseParameter: BaseParameter,
    changeInfoList: BaseParameterChangeInfo[],
    characterLevel: number
): void {
    changeInfoList.forEach((changeInfo) => {
        const type = changeInfo.baseParameterType;
        const current = getBaseParameterValue(baseParameter, type);
        switch (changeInfo.changeParameterType) {
            case ChangeParameterTypeEnum.Addition: {
                setBaseParameterValue(baseParameter, type, current + (changeInfo.value ?? 0));
                break;
            }
            case ChangeParameterTypeEnum.AdditionPercent: {
                const next = Math.trunc(current + current * (changeInfo.value ?? 0) * 0.0001);
                setBaseParameterValue(baseParameter, type, next);
                break;
            }
            case ChangeParameterTypeEnum.CharacterLevelConstantMultiplicationAddition: {
                setBaseParameterValue(baseParameter, type, current + characterLevel * (changeInfo.value ?? 0));
                break;
            }
            default:
                break;
        }
    });
}

function getBaseParameterValue(baseParameter: BaseParameter, type: BaseParameterType): number {
    switch (type) {
        case BaseParameterTypeEnum.Muscle:
            return baseParameter.muscle ?? 0;
        case BaseParameterTypeEnum.Energy:
            return baseParameter.energy ?? 0;
        case BaseParameterTypeEnum.Intelligence:
            return baseParameter.intelligence ?? 0;
        case BaseParameterTypeEnum.Health:
            return baseParameter.health ?? 0;
        default:
            return 0;
    }
}

function setBaseParameterValue(baseParameter: BaseParameter, type: BaseParameterType, value: number): void {
    switch (type) {
        case BaseParameterTypeEnum.Muscle:
            baseParameter.muscle = value;
            break;
        case BaseParameterTypeEnum.Energy:
            baseParameter.energy = value;
            break;
        case BaseParameterTypeEnum.Intelligence:
            baseParameter.intelligence = value;
            break;
        case BaseParameterTypeEnum.Health:
            baseParameter.health = value;
            break;
        default:
            break;
    }
}

function getBattleParameterDict(
    infoList: BattleParameterChangeInfo[],
    characterLevel: number
): { addParameters: Record<number, number>; mulParameters: Record<number, number> } {
    const addParameters: Record<number, number> = {};
    const mulParameters: Record<number, number> = {};

    BATTLE_PARAMETER_TYPES.forEach((type) => {
        addParameters[type] = 0;
        mulParameters[type] = 0;
    });

    infoList.forEach((info) => {
        const type = info.battleParameterType;
        if (!type) return;
        switch (info.changeParameterType) {
            case ChangeParameterTypeEnum.Addition:
                addParameters[type] += info.value ?? 0;
                break;
            case ChangeParameterTypeEnum.AdditionPercent:
                mulParameters[type] += info.value ?? 0;
                break;
            case ChangeParameterTypeEnum.CharacterLevelConstantMultiplicationAddition:
                addParameters[type] += (info.value ?? 0) * characterLevel;
                break;
            default:
                break;
        }
    });

    return { addParameters, mulParameters };
}

function createBattleParameter(
    defaultParameter: BattleParameter,
    baseParameter: BaseParameter,
    mainParameterType: BaseParameterType,
    playerRankMb: PlayerRankMB,
    addParameterMap: Record<number, number>,
    mulParameterMap: Record<number, number>
): BattleParameter {
    const hpBonus = playerRankMb?.hpBonus ?? 0;
    const hpPercentBonus = playerRankMb?.hpPercentBonus ?? 0;
    const attackPowerBonus = playerRankMb?.attackPowerBonus ?? 0;
    const defensePenetrationBonus = playerRankMb?.defensePenetrationBonus ?? 0;
    const damageEnhanceBonus = playerRankMb?.damageEnhanceBonus ?? 0;
    const hitBonus = playerRankMb?.hitBonus ?? 0;
    const criticalBonus = playerRankMb?.criticalBonus ?? 0;
    const debuffHitBonus = playerRankMb?.debuffHitBonus ?? 0;
    const speedBonus = playerRankMb?.speedBonus ?? 0;
    const criticalDamageEnhanceBonus = playerRankMb?.criticalDamageEnhanceBonus ?? 0;
    const damageReflectBonus = playerRankMb?.damageReflectBonus ?? 0;
    const hpDrainBonus = playerRankMb?.hpDrainBonus ?? 0;

    const battle: BattleParameter = {
        attackPower: 0,
        avoidance: 0,
        critical: 0,
        criticalDamageEnhance: 0,
        criticalResist: 0,
        damageEnhance: 0,
        damageReflect: 0,
        debuffHit: 0,
        debuffResist: 0,
        defense: 0,
        defensePenetration: 0,
        hit: 0,
        hP: 0,
        hpDrain: 0,
        magicCriticalDamageRelax: 0,
        magicDamageRelax: 0,
        physicalCriticalDamageRelax: 0,
        physicalDamageRelax: 0,
        speed: 0,
    } as BattleParameter;

    battle.speed = (defaultParameter.speed ?? 0) + (addParameterMap[BattleParameterTypeEnum.Speed] ?? 0) + speedBonus;

    battle.hP = Math.trunc(
        ((defaultParameter.hP ?? 0)
            + hpBonus
            + (addParameterMap[BattleParameterTypeEnum.Hp] ?? 0)
            + 10 * (baseParameter.health ?? 0))
        * (hpPercentBonus + (mulParameterMap[BattleParameterTypeEnum.Hp] ?? 0) + 10000.0) * 0.0001
    );

    battle.attackPower = attackPowerBonus + Math.trunc(
        ((defaultParameter.attackPower ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.AttackPower] ?? 0)
            + getBaseParameterValue(baseParameter, mainParameterType))
        * ((mulParameterMap[BattleParameterTypeEnum.AttackPower] ?? 0) + 10000.0) * 0.0001
    );

    battle.defense = Math.trunc(
        ((mulParameterMap[BattleParameterTypeEnum.Defense] ?? 0) + 10000)
        * (addParameterMap[BattleParameterTypeEnum.Defense] ?? 0) * 0.0001
        + (defaultParameter.defense ?? 0)
    );

    battle.physicalDamageRelax = Math.trunc(
        ((defaultParameter.physicalDamageRelax ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.PhysicalDamageRelax] ?? 0)
            + (baseParameter.muscle ?? 0))
        * ((mulParameterMap[BattleParameterTypeEnum.PhysicalDamageRelax] ?? 0) + 10000) * 0.0001
    );

    battle.magicDamageRelax = Math.trunc(
        ((defaultParameter.magicDamageRelax ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.MagicDamageRelax] ?? 0)
            + (baseParameter.intelligence ?? 0))
        * ((mulParameterMap[BattleParameterTypeEnum.MagicDamageRelax] ?? 0) + 10000) * 0.0001
    );

    battle.defensePenetration = (addParameterMap[BattleParameterTypeEnum.DefensePenetration] ?? 0)
        + defensePenetrationBonus
        + (defaultParameter.defensePenetration ?? 0);

    battle.damageEnhance = (addParameterMap[BattleParameterTypeEnum.DamageEnhance] ?? 0)
        + damageEnhanceBonus
        + (defaultParameter.damageEnhance ?? 0);

    battle.hit = Math.trunc(
        ((baseParameter.muscle ?? 0) * 0.5
            + (defaultParameter.hit ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.Hit] ?? 0)
            + hitBonus)
        * ((mulParameterMap[BattleParameterTypeEnum.Hit] ?? 0) + 10000) * 0.0001
    );

    battle.avoidance = Math.trunc(
        ((baseParameter.energy ?? 0) * 0.5
            + (defaultParameter.avoidance ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.Avoidance] ?? 0))
        * ((mulParameterMap[BattleParameterTypeEnum.Avoidance] ?? 0) + 10000) * 0.0001
    );

    battle.critical = Math.trunc(
        ((baseParameter.energy ?? 0) * 0.5
            + (defaultParameter.critical ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.Critical] ?? 0)
            + criticalBonus)
        * ((mulParameterMap[BattleParameterTypeEnum.Critical] ?? 0) + 10000) * 0.0001
    );

    battle.criticalResist = Math.trunc(
        ((baseParameter.health ?? 0) * 0.5
            + (defaultParameter.criticalResist ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.CriticalResist] ?? 0))
        * ((mulParameterMap[BattleParameterTypeEnum.CriticalResist] ?? 0) + 10000) * 0.0001
    );

    battle.hpDrain = (defaultParameter.hpDrain ?? 0)
        + Math.trunc(addParameterMap[BattleParameterTypeEnum.HpDrain] ?? 0)
        + hpDrainBonus;

    battle.damageReflect = (defaultParameter.damageReflect ?? 0)
        + Math.trunc(addParameterMap[BattleParameterTypeEnum.DamageReflect] ?? 0)
        + damageReflectBonus;

    battle.criticalDamageEnhance = (defaultParameter.criticalDamageEnhance ?? 0)
        + (addParameterMap[BattleParameterTypeEnum.CriticalDamageEnhance] ?? 0)
        + criticalDamageEnhanceBonus;

    battle.physicalCriticalDamageRelax = Math.trunc(defaultParameter.criticalDamageEnhance ?? 0)
        + Math.trunc(addParameterMap[BattleParameterTypeEnum.PhysicalCriticalDamageRelax] ?? 0);

    battle.magicCriticalDamageRelax = Math.trunc(defaultParameter.criticalDamageEnhance ?? 0)
        + Math.trunc(addParameterMap[BattleParameterTypeEnum.MagicCriticalDamageRelax] ?? 0);

    battle.debuffHit = Math.trunc(
        ((baseParameter.intelligence ?? 0) * 0.5
            + (defaultParameter.debuffHit ?? 0)
            + (addParameterMap[BattleParameterTypeEnum.DebuffHit] ?? 0)
            + debuffHitBonus)
        * ((mulParameterMap[BattleParameterTypeEnum.DebuffHit] ?? 0) + 10000) * 0.0001
    );

    battle.debuffResist = Math.trunc(
        (addParameterMap[BattleParameterTypeEnum.DebuffResist] ?? 0)
        * ((mulParameterMap[BattleParameterTypeEnum.DebuffResist] ?? 0) + 10000) * 0.0001
    );

    return battle;
}

function calculateBattlePower(battleParameter: BattleParameter, baseParameter: BaseParameter): number {
    const coeff = (type: BattleParameterType): number => {
        switch (type) {
            case BattleParameterTypeEnum.Hp:
                return 0.05;
            case BattleParameterTypeEnum.AttackPower:
                return 2.0;
            case BattleParameterTypeEnum.PhysicalDamageRelax:
                return 1.5;
            case BattleParameterTypeEnum.MagicDamageRelax:
                return 1.5;
            case BattleParameterTypeEnum.Hit:
                return 1.0;
            case BattleParameterTypeEnum.Avoidance:
                return 1.0;
            case BattleParameterTypeEnum.DebuffHit:
                return 1.0;
            case BattleParameterTypeEnum.DebuffResist:
                return 1.0;
            case BattleParameterTypeEnum.Critical:
                return 3.0;
            case BattleParameterTypeEnum.CriticalResist:
                return 3.0;
            case BattleParameterTypeEnum.CriticalDamageEnhance:
                return 2000.0;
            case BattleParameterTypeEnum.PhysicalCriticalDamageRelax:
                return 2000.0;
            case BattleParameterTypeEnum.MagicCriticalDamageRelax:
                return 2000.0;
            case BattleParameterTypeEnum.DefensePenetration:
                return 7.0;
            case BattleParameterTypeEnum.DamageEnhance:
                return 7.0;
            case BattleParameterTypeEnum.Defense:
                return 2.33;
            case BattleParameterTypeEnum.DamageReflect:
                return 1500.0;
            case BattleParameterTypeEnum.HpDrain:
                return 1500.0;
            case BattleParameterTypeEnum.Speed: {
                const sum = (baseParameter.energy ?? 0) + (baseParameter.health ?? 0)
                    + (baseParameter.intelligence ?? 0) + (baseParameter.muscle ?? 0);
                return sum * 0.000125;
            }
            default:
                return 0.0;
        }
    };

    return Math.trunc(
        (battleParameter.hP ?? 0) * coeff(BattleParameterTypeEnum.Hp)
        + (battleParameter.attackPower ?? 0) * coeff(BattleParameterTypeEnum.AttackPower)
        + (battleParameter.defense ?? 0) * coeff(BattleParameterTypeEnum.Defense)
        + (battleParameter.physicalDamageRelax ?? 0) * coeff(BattleParameterTypeEnum.PhysicalDamageRelax)
        + (battleParameter.magicDamageRelax ?? 0) * coeff(BattleParameterTypeEnum.MagicDamageRelax)
        + (battleParameter.damageEnhance ?? 0) * coeff(BattleParameterTypeEnum.DamageEnhance)
        + (battleParameter.defensePenetration ?? 0) * coeff(BattleParameterTypeEnum.DefensePenetration)
        + (battleParameter.hit ?? 0) * coeff(BattleParameterTypeEnum.Hit)
        + (battleParameter.avoidance ?? 0) * coeff(BattleParameterTypeEnum.Avoidance)
        + (battleParameter.critical ?? 0) * coeff(BattleParameterTypeEnum.Critical)
        + (battleParameter.criticalResist ?? 0) * coeff(BattleParameterTypeEnum.CriticalResist)
        + (battleParameter.hpDrain ?? 0) * 0.01 * coeff(BattleParameterTypeEnum.HpDrain)
        + (battleParameter.damageReflect ?? 0) * 0.01 * coeff(BattleParameterTypeEnum.DamageReflect)
        + (battleParameter.criticalDamageEnhance ?? 0) * 0.01 * coeff(BattleParameterTypeEnum.CriticalDamageEnhance)
        + (battleParameter.physicalCriticalDamageRelax ?? 0) * 0.01 * coeff(BattleParameterTypeEnum.PhysicalCriticalDamageRelax)
        + (battleParameter.magicCriticalDamageRelax ?? 0) * 0.01 * coeff(BattleParameterTypeEnum.MagicCriticalDamageRelax)
        + (battleParameter.debuffHit ?? 0) * coeff(BattleParameterTypeEnum.DebuffHit)
        + (battleParameter.debuffResist ?? 0) * coeff(BattleParameterTypeEnum.DebuffResist)
        + (battleParameter.speed ?? 0) * coeff(BattleParameterTypeEnum.Speed)
    );
}

function createEmptyBaseParameter(): BaseParameter {
    return { energy: 0, health: 0, intelligence: 0, muscle: 0 } as BaseParameter;
}

function createEmptyBattleParameter(): BattleParameter {
    return {
        attackPower: 0,
        avoidance: 0,
        critical: 0,
        criticalDamageEnhance: 0,
        criticalResist: 0,
        damageEnhance: 0,
        damageReflect: 0,
        debuffHit: 0,
        debuffResist: 0,
        defense: 0,
        defensePenetration: 0,
        hit: 0,
        hP: 0,
        hpDrain: 0,
        magicCriticalDamageRelax: 0,
        magicDamageRelax: 0,
        physicalCriticalDamageRelax: 0,
        physicalDamageRelax: 0,
        speed: 0,
    } as BattleParameter;
}

function cloneBattleParameter(parameter: BattleParameter): BattleParameter {
    return {
        attackPower: parameter.attackPower ?? 0,
        avoidance: parameter.avoidance ?? 0,
        critical: parameter.critical ?? 0,
        criticalDamageEnhance: parameter.criticalDamageEnhance ?? 0,
        criticalResist: parameter.criticalResist ?? 0,
        damageEnhance: parameter.damageEnhance ?? 0,
        damageReflect: parameter.damageReflect ?? 0,
        debuffHit: parameter.debuffHit ?? 0,
        debuffResist: parameter.debuffResist ?? 0,
        defense: parameter.defense ?? 0,
        defensePenetration: parameter.defensePenetration ?? 0,
        hit: parameter.hit ?? 0,
        hP: parameter.hP ?? 0,
        hpDrain: parameter.hpDrain ?? 0,
        magicCriticalDamageRelax: parameter.magicCriticalDamageRelax ?? 0,
        magicDamageRelax: parameter.magicDamageRelax ?? 0,
        physicalCriticalDamageRelax: parameter.physicalCriticalDamageRelax ?? 0,
        physicalDamageRelax: parameter.physicalDamageRelax ?? 0,
        speed: parameter.speed ?? 0,
    } as BattleParameter;
}

function buildMasterMaps(tables: CharacterDetailCalculationContext['masters']): MasterTableMaps {
    const characterById: Record<number, CharacterMB> = {};
    tables.characterTable.forEach((item) => { characterById[item.id] = item; });

    const equipmentById: Record<number, EquipmentMB> = {};
    tables.equipmentTable.forEach((item) => { equipmentById[item.id] = item; });

    const sphereById: Record<number, SphereMB> = {};
    tables.sphereTable.forEach((item) => { sphereById[item.id] = item; });

    const equipmentSetById: Record<number, EquipmentSetMB> = {};
    tables.equipmentSetTable.forEach((item) => { equipmentSetById[item.id] = item; });

    const equipmentExclusiveEffectByIdAndCharacter: Record<string, EquipmentExclusiveEffectMB> = {};
    tables.equipmentExclusiveEffectTable.forEach((item) => {
        const key = `${item.id}_${item.characterId}`;
        equipmentExclusiveEffectByIdAndCharacter[key] = item;
    });

    const equipmentReinforcementByLevel: Record<number, EquipmentReinforcementParameterMB> = {};
    tables.equipmentReinforcementParameterTable.forEach((item) => {
        equipmentReinforcementByLevel[item.id] = item;
    });

    const equipmentLegendSacredTreasureByLevel: Record<number, EquipmentLegendSacredTreasureMB> = {};
    tables.equipmentLegendSacredTreasureTable.forEach((item) => {
        equipmentLegendSacredTreasureByLevel[item.lv] = item;
    });

    const equipmentMatchlessSacredTreasureByLevel: Record<number, EquipmentMatchlessSacredTreasureMB> = {};
    tables.equipmentMatchlessSacredTreasureTable.forEach((item) => {
        equipmentMatchlessSacredTreasureByLevel[item.lv] = item;
    });

    const characterCollectionById: Record<number, CharacterCollectionMB> = {};
    tables.characterCollectionTable.forEach((item) => { characterCollectionById[item.id] = item; });

    const characterCollectionLevelByCollectionLevel: Record<string, CharacterCollectionLevelMB> = {};
    tables.characterCollectionLevelTable.forEach((item) => {
        const key = `${item.collectionId}_${item.collectionLevel}`;
        characterCollectionLevelByCollectionLevel[key] = item;
    });

    const characterPotentialByLevelAndSubLevel: Record<string, CharacterPotentialMB> = {};
    tables.characterPotentialTable.forEach((item) => {
        const key = `${item.characterLevel}_${item.characterSubLevel}`;
        characterPotentialByLevelAndSubLevel[key] = item;
    });

    const characterPotentialCoefficientByInitialAndNow: Record<string, CharacterPotentialCoefficientMB> = {};
    tables.characterPotentialCoefficientTable.forEach((item) => {
        const key = `${item.initialRarityFlags}_${item.rarityFlags}`;
        characterPotentialCoefficientByInitialAndNow[key] = item;
    });

    const playerRankByRank: Record<number, PlayerRankMB> = {};
    tables.playerRankTable.forEach((item) => { playerRankByRank[item.rank] = item; });

    return {
        characterById,
        equipmentById,
        sphereById,
        equipmentSetById,
        equipmentExclusiveEffectByIdAndCharacter,
        equipmentReinforcementByLevel,
        equipmentLegendSacredTreasureByLevel,
        equipmentMatchlessSacredTreasureByLevel,
        characterCollectionById,
        characterCollectionLevelByCollectionLevel,
        characterPotentialByLevelAndSubLevel,
        characterPotentialCoefficientByInitialAndNow,
        playerRankByRank,
    };
}
